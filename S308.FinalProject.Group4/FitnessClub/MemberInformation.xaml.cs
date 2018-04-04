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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "Membership Type:".PadRight(12) + "Gold" + Environment.NewLine +
                "StartDate:".PadRight(12) + "01/01/2018" + Environment.NewLine + "EndDate:".PadRight(12) + "01/01/2019" + Environment.NewLine
                + "Membership Cost:".PadRight(12) + "$35" + Environment.NewLine
                + "Subtotal:".PadRight(12) + "$420" + Environment.NewLine
                + "Additional Features:".PadRight(12) + Environment.NewLine + "Personal Training Plan".PadRight(12).PadLeft(5) + "$200" + Environment.NewLine
                + "Total:".PadRight(12) + "$620" + Environment.NewLine;
        }
    }
}
