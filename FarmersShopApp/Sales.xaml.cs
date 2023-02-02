using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Collections;

namespace FarmersShopApp
{
    public partial class Sales : Window
    {
        SqlConnection con;
        public string[] productNames { get; set; }
        public string updateSqls { get; set; }
        public double totalAll { get; set; }

        public Sales()
        {

            InitializeComponent();
            string connectionString = "Data Source=DESKTOP-98IF6CD;Initial Catalog=FamersProduct;Integrated Security=True";
            con = new SqlConnection(connectionString);

            productNames = GetProductData();
            DataContext = this;
        }

        private string[] GetProductData()
        {
            con.Open();

            string querySql = "select ProductName from FarmersProduct";
            SqlCommand cmd = new SqlCommand(querySql, con);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            ArrayList productNameList = new ArrayList();

            int counter = 0;

            while (sqlDataReader.Read())
            {
                //get rows
                counter++;
                productNameList.Add(sqlDataReader.GetValue(0).ToString());
            }

            string[] productNames = new string[counter];

            for (int i = 0; i < productNameList.Count; i++)
            {
                productNames[i] = (string)productNameList[i];

            }

            con.Close();
            return productNames;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (this.productComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("please select product what you want.");
                return;
            }
            if (this.AmountTextBox.Text.Length == 0)
            {
                MessageBox.Show("please input the amount what you need.");
                return;
            }

            string productName = (string)productComboBox.SelectedItem;
            double amount = Convert.ToDouble(this.AmountTextBox.Text);
            double price = 0;
            double total = 0;

            con.Open();
            string querySql = "select ProductName, Amount, Price from FarmersProduct where ProductName=@ProductName";
            SqlCommand cmd = new SqlCommand(querySql, con);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            double amountInventory = 0;
            while (sqlDataReader.Read())
            {
                amountInventory = Convert.ToDouble(sqlDataReader.GetValue(1).ToString());
                price = Convert.ToDouble(sqlDataReader.GetValue(2).ToString());
            }

            if (amount > amountInventory)
            {
                MessageBox.Show("this product is out of stock, please reduce the amount.");
                con.Close();
                return;
            }

            updateSqls = updateSqls + "update FarmersProduct set Amount = Amount - " + this.AmountTextBox.Text + "where ProductName = '" + this.productComboBox.SelectedItem + "'; ";
            total = Math.Round(amount * price, 2);
            if (this.BillTextBox.Text.Length == 0)
            {
                this.BillTextBox.Text = productName + "   " + amount.ToString() + "     $" + total.ToString() + "\n";
                totalAll = Math.Round(totalAll + total, 2);
                this.BillTextBox.Text = this.BillTextBox.Text + "Total price: " + totalAll;
            }
            else
            {
                string tempStr = this.BillTextBox.Text;

                tempStr = tempStr.Substring(0, tempStr.Length - (tempStr.Length - tempStr.LastIndexOf("\n")));
                tempStr = tempStr + "\n";
                tempStr = tempStr + productName + "    " + amount.ToString() + "    $" + total.ToString() + "\n";
                totalAll += total;

                this.BillTextBox.Text = tempStr + "total price: " + "$" + totalAll;

            }
            this.productComboBox.SelectedIndex = -1;
            this.AmountTextBox.Text = "";

            con.Close();
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(updateSqls, con);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            updateSqls = "";
            totalAll = 0;

            MessageBox.Show(this.BillTextBox.Text);
            this.BillTextBox.Clear();
            con.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWin = new MainWindow();
            this.Close();
            MainWin.Show();
        }
    }
}
