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
    /// Interaction logic for MemberInformation.xaml
    /// </summary>
    public partial class MemberInformation : Window
    {
        public MemberInformation()
        {
            InitializeComponent();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
                txtOutput.Text = "Membership Type:".PadRight(12) + "Gold" + Environment.NewLine +
                    "StartDate:".PadRight(12) + "01/01/2018" + Environment.NewLine + "EndDate:".PadRight(12) + "01/01/2019" + Environment.NewLine
                    + "Membership Cost:".PadRight(12) + "$35" + Environment.NewLine
                    + "Subtotal:".PadRight(12) + "$420" + Environment.NewLine
                    + "Additional Features:".PadRight(12) + Environment.NewLine + "Personal Training Plan".PadRight(12).PadLeft(5) + "$200" + Environment.NewLine
                    + "Total:".PadRight(12) + "$620" + Environment.NewLine;
            txtOutput.Text = txtOutput.Text + "First Name".PadRight(12) + "Last Name".PadRight(12) + "Membership Type".PadRight(12) + Environment.NewLine;
            txtOutput.Text = txtOutput.Text + "Boy".PadRight(12) + "Martin".PadRight(12) + "Gold".PadRight(12) + Environment.NewLine;
            txtOutput.Text = txtOutput.Text + "Expiration Date".PadRight(12) + "Phone".PadRight(12) + "e-Mail".PadRight(12) + Environment.NewLine;
            txtOutput.Text = txtOutput.Text + "01/01/2019".PadRight(12) + "4448571345".PadRight(12) + "bmartin@iu.edu".PadRight(12) + Environment.NewLine;
             txtOutput.Text = txtOutput.Text + "Gender".PadRight(12) + "Age".PadRight(12) + "Weight".PadRight(12) + Environment.NewLine +
                 "Male".PadRight(12) + "20".PadRight(12) + "180".PadRight(12) + Environment.NewLine;
            

        }

        private void btnHomeFromPM_Click(object sender, RoutedEventArgs e)
        {
            //When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }
    }
}
