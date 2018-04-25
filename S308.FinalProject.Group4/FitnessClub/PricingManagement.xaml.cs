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
    /// Interaction logic for PricingManagement.xaml
    /// </summary>
    public partial class PricingManagement : Window
    {
        List<Pricing> pricingList;
        public PricingManagement()
        {
            InitializeComponent();
            pricingList = new List<Pricing>();
            ImportPricingData();
        }

        private void ImportPricingData()
        {
            string strFilePath = @"..\..\..\MembershipPricing.json";

            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);

                if (pricingList.Count>=0)
                {
                    MessageBox.Show(pricingList.Count + " Items have been Imported");
                }
                else
                {
                    MessageBox.Show("No items were imported. Please Check the Data File");
                }

                foreach (var s in pricingList)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Name = "cbi" + s.MembershipType;
                    item.Content = s.MembershipType;
                    cbxMembershipType.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in importing Membership Pricing: " + ex.Message);
            }
        }



        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            double NewPrice;
            if (!Double.TryParse(txtNewPriceValue.Text, out NewPrice))
            {
                MessageBox.Show("Please enter a valid value as price");
            }
        }

        private void btnHomeFromPM_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }
    
    }
}