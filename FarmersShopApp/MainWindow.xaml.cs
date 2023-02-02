using System;
using System.Collections.Generic;
using System.Data;
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

namespace FarmersShopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-98IF6CD;Initial Catalog=FamersProduct;Integrated Security=True;Pooling=False");
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }


        private void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from FarmersProduct", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string query = "select ProductName,Amount,Price from FarmersProduct where ProductID=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(ProductID.Text));
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductName.Text = (string)sqlDataReader.GetValue(0);
                    Amount.Text = sqlDataReader.GetValue(1).ToString();
                    Price.Text = sqlDataReader.GetValue(2).ToString();
                }
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
                //openthe database connection
                con.Open();
                string query = "insert into FarmersProduct values(@ProductId,@ProductName,@Amount,@Price)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductId", int.Parse(@ProductID.Text));//we need to call the textbox name and the grab the text
                cmd.Parameters.AddWithValue("@ProductName", ProductName.Text);
                cmd.Parameters.AddWithValue("@Amount", Amount.Text);
                cmd.Parameters.AddWithValue("@Price", Price.Text);
                //we now need to execute our query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserted Perfectlly to the Database");
                con.Close();
                LoadGrid();
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
                //open the database connection
                con.Open();
                string query = "Update FarmersProduct set ProductName=@ProductName,Amount=@Amount,Price=@Price where ProductID=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(ProductID.Text));//we need to call the textbox name and the grab the text
                cmd.Parameters.AddWithValue("@ProductName", ProductName.Text);
                cmd.Parameters.AddWithValue("@Amount", Amount.Text);
                cmd.Parameters.AddWithValue("@Price", Price.Text);
                //we now need to execute our query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Perfectlly to the Database");
                con.Close();
                LoadGrid();
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
                //open the database connection
                con.Open();
                string query = "delete FarmersProduct where ProductID=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(ProductID.Text));
                //we now need to execute our query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delelted Perfectlly to the Database");
                con.Close();
                LoadGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGrid.Items.Count > 0)
                { 
                    ProductID.Text = ((DataRowView)dataGrid.SelectedItem).Row["ProductID"].ToString();
                    ProductName.Text = ((DataRowView)dataGrid.SelectedItem).Row["Productname"].ToString();
                    Amount.Text = ((DataRowView)dataGrid.SelectedItem).Row["Amount"].ToString();
                    Price.Text = ((DataRowView)dataGrid.SelectedItem).Row["Price"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void clearData()
        {
            ProductID.Clear();
            ProductName.Clear();
            Amount.Clear();
            Price.Clear();  
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sales swin = new Sales();
            swin.Show();
            this.Close();
        }
    }
}
