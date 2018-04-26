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
using Newtonsoft.Json;
using System.IO;

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MembershipSales.xaml
    /// </summary>
    public partial class MembershipSales : Window
    {
        List<Pricing> pricingList;
        public MembershipSales()
        {
            InitializeComponent();
            pricingList = new List<Pricing>();
            ImportPricingData();

        }
        //Load membership type from json file to combobox
        private void ImportPricingData()
        {
            string strFilePath = @"..\..\..\MembershipPricing.json";
            
            try
            {
                string jsonData = File.ReadAllText(strFilePath);

                pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);

                foreach (var s in pricingList)
                {   
                    ComboBoxItem item = new ComboBoxItem();
                    item.Name = "cbi" + s.MembershipType.Substring(0, s.MembershipType.IndexOf(" ")) + s.MembershipType.Substring(s.MembershipType.IndexOf(" ") + 1, 2).Trim();
                    item.Content = s.MembershipType;
                    cbbMembershipType.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in importing Membership Pricing: " + ex.Message);
            }
        }


        private void btnHomeFromPM_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }

        private void btnQuote_Click(object sender, RoutedEventArgs e)
        {//Validation membership type is required
            if(cbbMembershipType.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a membership type!");
                return;
            }

            //Validation start date is not in the past
            DateTime StartDate = Convert.ToDateTime(dpiStartDate.Text);
            DateTime TodayDate = DateTime.Today;
            
            if(StartDate < TodayDate)
            {
                MessageBox.Show("Start date must be later than today's date!");
                return;
            }

            
        }
    }
}
