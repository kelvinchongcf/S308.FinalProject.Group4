using System;
using System.Collections.Generic;
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

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        //Kelvin Chong, Willy Galvin, Sanyam Gupta
        //Logo took from Google Images https://www.google.com/search?tbm=isch&source=hp&biw=1536&bih=758&ei=w2bLWsGoCMXTjwSx85HACg&q=fitness+logo&oq=fitness+logo&gs_l=img.3..35i39k1j0l9.1986.3317.0.3488.14.14.0.0.0.0.98.761.12.12.0....0...1ac.1.64.img..2.12.760.0...0.LfmbwjvM2bQ#imgrc=yy8lSf_eskyi9M:
        {
            InitializeComponent();
        }

        private void btnMembershipSalesWindow_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MembershipSales().Show();
            this.Close();
        }

        private void btnPricingManagementWindow_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new PricingManagement().Show();
            this.Close();
        }

        private void btnMembershipInfoWindow_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MemberInformation().Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {//Close the progam when clicked
            this.Close();
        }

        private void btnExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
