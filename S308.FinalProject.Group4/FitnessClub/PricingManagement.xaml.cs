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
        //creating a list 
        List<Pricing> pricingList;
        public PricingManagement()
        {
            InitializeComponent();
            // creating a new instance of the pricing list
            pricingList = new List<Pricing>();

            //calling the function to import pricing data from JSON file
            ImportPricingData();
        }

        private void ImportPricingData()
        {
            //imported data from JSON file
            string strFilePath = @"..\..\..\MembershipPricing.json";

            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);

                //showing whether data was successfully imported or not
                if (pricingList.Count>=0)
                {
                    MessageBox.Show(pricingList.Count + " Items have been Imported");
                }
                else
                {
                    MessageBox.Show("No items were imported. Please Check the Data File");
                }


                //creating combo box items to fill the combo box based on the values in the JSON file
                foreach (var s in pricingList)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Name = "cbi" + s.MembershipType.Substring(0,s.MembershipType.IndexOf(" ")) + s.MembershipType.Substring(s.MembershipType.IndexOf(" ")+1,2).Trim();
                    item.Content = s.MembershipType;
                    cbxMembershipType.Items.Add(item);

                    // displaying the price and availability of the item which is already selected
                    if (item.IsSelected)
                    {
                        lblOldPriceValue.Content = s.Price.ToString("C2");
                        lblOldAvailabilityCheck.Content = s.Availability;
                    }
                }
            }
            catch (Exception ex)
            {
                //error message when import is unsuccessful
                MessageBox.Show("Error in importing Membership Pricing: " + ex.Message);
            }
        }



        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //validating if the number entered in price is a integer or not
            double NewPrice;
            if (!Double.TryParse(txtNewPriceValue.Text, out NewPrice))
            {
                MessageBox.Show("Please enter a valid value as price");
                return;
            }

            //validating if the number entered in price box is positive or not and if the text box is empty or not
            if(txtNewPriceValue.Text=="")
            {
                MessageBox.Show("Please enter a valid value as price");
                return;
            }
                else if (Convert.ToDouble(txtNewPriceValue.Text)<=0)
                {
                    MessageBox.Show("Please enter a valid value as price");
                return;
                }
                

            // declaring variable to store the new price and identify the membership type selected
            
            string SelectedItem;
            double ChangedPrice;
            SelectedItem = cbxMembershipType.SelectedValue.ToString().Substring(cbxMembershipType.SelectedValue.ToString().IndexOf(":") + 1).Trim();
            ChangedPrice = Convert.ToDouble(txtNewPriceValue.Text);
            // matching the membership type selected to the particular instance
            //changing the availability and price of the membership type selected
            foreach (var s in pricingList)
            {
                if (s.MembershipType == SelectedItem)
                {
                    s.Price = ChangedPrice;
                    
                    if(rdbYesAvailability.IsChecked == true)
                    {
                        s.Availability = "Yes";
                    }

                    else if(rdbNoAvailability.IsChecked == true)
                    {
                        s.Availability = "No";
                    }
                }
            }

            //writing or saving the changes made to membership type, price and availability to the JSON file for future purposes

            try
            { string jsonData = JsonConvert.SerializeObject(pricingList);
              System.IO.File.WriteAllText(@"..\..\..\MembershipPricing.json", jsonData);
                //message to be shown when the export is successful
                MessageBox.Show("Changes saved");
            }

            catch(Exception ex)
            {
                //message to be shown when the export is unsuccessful
                MessageBox.Show("Changes Could Not be Saved");
            }

        }
     
        private void btnHomeFromPM_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }

        private void btnSearchInfo_Click(object sender, RoutedEventArgs e)
        {
            //idenitfying the membership type selected to the particular instance from the list declared
            string SelectedItem;
            SelectedItem = cbxMembershipType.SelectedValue.ToString().Substring(cbxMembershipType.SelectedValue.ToString().IndexOf(":")+1).Trim();

            //displaying the price and availability of the memberhip type selected
            foreach (var s in pricingList)
            {
                if (SelectedItem == s.MembershipType)
                {
                    lblOldPriceValue.Content = s.Price.ToString("C2");
                    lblOldAvailabilityCheck.Content = s.Availability;
                }
            }
        }

        private void txtNewPriceValue_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}