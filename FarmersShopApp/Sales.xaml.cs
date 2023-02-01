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

namespace FarmersShopApp
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-98IF6CD;Initial Catalog=FamersProduct;Integrated Security=True;Pooling=False");
        public Sales()
        {
            InitializeComponent();
            LoadProListGrid();
            LoadShoppingListGrid();
        }

       

        private void LoadProListGrid()
        {
            SqlCommand cmd = new SqlCommand("select ProductName,Price from FarmersProduct", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            productList.ItemsSource = dt.DefaultView;
        }

        private void LoadShoppingListGrid()
        {
            SqlCommand cmd = new SqlCommand("select ProductName,OrderAmout,TotalPrice from ShoppingList", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            ShoppingList.ItemsSource = dt.DefaultView;
        }

        //public void clearShoppingListData()
        //{
            
        //    ProductName.Clear();
        //    Amount.Clear();
        //    Price.Clear();
        //}

        //private void clear_Click(object sender, RoutedEventArgs e)
        //{
        //    clearShoppingListData();
            
        //}

        //private void add_Click(object sender, RoutedEventArgs e)
        //{
           
        //}
    }
}
