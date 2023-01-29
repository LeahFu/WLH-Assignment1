using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FarmerMarket
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Admin : Window
    {
        SqlConnection con;
        public Admin()
        {
            InitializeComponent();
        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {
            try//Exception Handling
            {
                string connectionString = "Data Source=IT\\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True";
                con = new SqlConnection(connectionString);
                con.Open();
                MessageBox.Show("Connection Established Properly");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Open the Database Connection
                con.Open();
                string query = "insert into ProductsInventory values(@ProductID, @ProductName, @Amount, @Price)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", int.Parse(ProductID.Text));
                cmd.Parameters.AddWithValue("@ProductName", ProductName.Text);
                cmd.Parameters.AddWithValue("@Amount", double.Parse(Amount.Text));
                cmd.Parameters.AddWithValue("@Price", double.Parse(Price.Text));
                //We now need to execute our Query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserted Perfectly to the Database");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Open the Database Connection
                con.Open();
                string query = "Update ProductsInventory set ProductName=@ProductName, Amount=@Amount, Price=@Price where ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", int.Parse(ProductID.Text));
                cmd.Parameters.AddWithValue("@ProductName", ProductName.Text);
                cmd.Parameters.AddWithValue("@Amount", double.Parse(Amount.Text));
                cmd.Parameters.AddWithValue("@Email", double.Parse(Price.Text));
                //We now need to execute our Query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserted Perfectly to the Database");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string Query = "SELECT ProductName, Amount, Price from ProductsInventory where ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@ProductID", int.Parse(ProductID.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    ProductName.Text = (string)sqlReader.GetValue(0);
                    Amount.Text = sqlReader.GetValue(1).ToString();
                    Price.Text = (string)sqlReader.GetValue(2);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string Query = "DELETE ProductsInventory where ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@ProductID", int.Parse(ProductID.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Properly");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
