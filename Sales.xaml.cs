using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace FarmerMarket
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        SqlConnection con = new SqlConnection("Data Source=IT\\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True");

        public string[] operations { get; set; }

        ArrayList prices = new ArrayList();
        double finalPrices;

        public Sales()
        {
            InitializeComponent();
            operations = new string[] { "Apple", "Orange", "Raspberry", "Blueberry", "Cauliflower" };
            DataContext = this;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            double amo = Convert.ToDouble(amount.Text);          
            double price;
            if (comboBox.SelectedIndex == 0)
            {
                price = amo * 2.10;
                prices.Add(price);
                result.Text = "Apple: " + amo + " * 2.10 = " + price.ToString();
            }
            if ((string)comboBox.SelectedValue == "Orange")
            {
                price = amo * 2.49;
                prices.Add(price);
                result.Text = "Orange: " + amo + " * 2.49 = " + price.ToString();
            }
            if (comboBox.SelectedIndex == 2)
            {
                price = amo * 2.35;
                prices.Add(price);
                result.Text = "Raspberry: " + amo + " * 2.35 = " + price.ToString();
            }
            if (comboBox.SelectedIndex == 3)
            {
                price = amo * 1.45;
                prices.Add(price);
                result.Text = "Blueberry: " + amo + " * 1.45 = " + price.ToString();
            }
            if (comboBox.SelectedIndex == 4)
            {
                price = amo * 2.22;
                prices.Add(price);
                result.Text = "Cauliflower: " + amo + " * 2.22 = " + price.ToString();
            }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
