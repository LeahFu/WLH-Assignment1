using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace FarmerMarket
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        SqlConnection con;
        public Sales()
        {
            InitializeComponent();
        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {
            try//Exception Handling
            {
                string connectionString = "Data Source=Wei;Initial Catalog=userDB;Integrated Security=True";
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

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Open the Database Connection
                con.Open();
                string query = "Update ProductsInventory set Amount= (Amount- @Amount)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Amount", double.Parse(Amount.Text));
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

        private void viewdata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string Query = "Select * from ProductsInventory";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGrid.ItemsSource = dt.AsDataView();
                DataContext = da;
                con.Close();
            }
            catch (SqlException ex)
            {
            }
        
        }
    }
}
