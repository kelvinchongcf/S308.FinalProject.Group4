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

            //Input area disabled until quote preview
            txtFirstName.IsEnabled = false;
            txtLastName.IsEnabled = false;
            cbbCreditCardType.IsEnabled = false;
            txtCreditCardNumber.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtEmail.IsEnabled = false;
            cbbGender.IsEnabled = false;
            txtAge.IsEnabled = false;
            txtWeight.IsEnabled = false;
            cbbPersonalGoal.IsEnabled = false;

        }
        //define path for new member info
        string strMembersPath = @"..\..\..\data.json";
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
            if (cbbMembershipType.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a membership type!");
                return;
            }

            //Validation start date is not in the past
            DateTime dtiStartDate;
            if (!DateTime.TryParse(dpiStartDate.Text, out dtiStartDate))
            {
                MessageBox.Show("Please select a start date!");
                return;
            }
            DateTime dtiTodayDate = DateTime.Today;

            if (dtiStartDate < dtiTodayDate)
            {
                MessageBox.Show("Start date must be later than today's date!");
                return;
            }

            //Calculation for quote price
            //declare variables to hold calculated values
            double dblQuotePrice;
            double dblMembershipPrice;
            double dblNumberOfMonths;
            int intNumberOfDays;
            double dblNumberOfDays;
            double dblMembershipCostPerMonth;
            double dblSubtotal;
            double dblAddPTP;
            double dblAddLR;

            //Calculation for MembershipType
            if (cbbMembershipType.SelectedIndex == 0)
            {
                dblMembershipPrice = 9.99;
            }
            else if (cbbMembershipType.SelectedIndex == 1)
            {
                dblMembershipPrice = 100.00;
            }
            else if (cbbMembershipType.SelectedIndex == 2)
            {
                dblMembershipPrice = 14.99;
            }
            else if (cbbMembershipType.SelectedIndex == 3)
            {
                dblMembershipPrice = 150.00;
            }
            else if (cbbMembershipType.SelectedIndex == 4)
            {
                dblMembershipPrice = 19.99;
            }
            else
            {
                dblMembershipPrice = 200.00;
            }

            lblCalcCostPerMonth.Content = dblMembershipPrice.ToString("c2");

            //Show the start date user selected
            lblShowStartDateAnswer.Content = dpiStartDate.SelectedDate;

            
            //Months to use
            if (cbbMembershipType.SelectedIndex == 0 || cbbMembershipType.SelectedIndex == 2 || cbbMembershipType.SelectedIndex == 4)
            {
                dblNumberOfMonths = 1;
            }
            else dblNumberOfMonths = 12;

            //Show the end date
            lblShowEndDateAnswer.Content = dtiStartDate.AddMonths(Convert.ToInt16(dblNumberOfMonths));

            //Calculating Subtotal
            
            lblCalcSubtotal.Content = (dblMembershipPrice * dblNumberOfMonths).ToString("c2");
            //Calculate Additional Cost
            if (cboPersonalTraining.IsChecked == true)
            {
                dblAddPTP = 5.00;
            }
            else dblAddPTP = 0;

            if (cboLocker.IsChecked == true)
            {
                dblAddLR = 1.00;
            }
            else dblAddLR = 0;

            lblCalcAddCost.Content = ((dblAddPTP + dblAddLR) * dblNumberOfMonths).ToString("c2");

            //Calculate Total Cost
            lblCalcTotalCost.Content = (((dblMembershipPrice * dblNumberOfMonths) + ((dblAddPTP + dblAddLR) * dblNumberOfMonths)).ToString("c2"));

            //Validation input areas after quote preview
            string strInsideQuoteBox;
            strInsideQuoteBox = Convert.ToString(lblCalcTotalCost.Content);

            if (strInsideQuoteBox == "")
            {
                txtFirstName.IsEnabled = false;
                txtLastName.IsEnabled = false;
                cbbCreditCardType.IsEnabled = false;
                txtCreditCardNumber.IsEnabled = false;
                txtPhone.IsEnabled = false;
                txtEmail.IsEnabled = false;
                cbbGender.IsEnabled = false;
                txtAge.IsEnabled = false;
                txtWeight.IsEnabled = false;
                cbbPersonalGoal.IsEnabled = false;
            }
            else
                txtFirstName.IsEnabled = true;
                txtLastName.IsEnabled = true;
                cbbCreditCardType.IsEnabled = true;
                txtCreditCardNumber.IsEnabled = true;
                txtPhone.IsEnabled = true;
                txtEmail.IsEnabled = true;
                cbbGender.IsEnabled = true;
                txtAge.IsEnabled = true;
                txtWeight.IsEnabled = true;
                cbbPersonalGoal.IsEnabled = true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Validation for inputs
            //Validation for first name
            if(txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter a first name!");
                return;
            }

            //validation for last name
            if(txtLastName.Text == "")
            {
                MessageBox.Show("Please enter a last name!");
                return;
            }

            //validation for credit card type
            if(cbbCreditCardType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a credit card type!");
                return;
            }
            //validation for credit card number
            if(txtCreditCardNumber.Text == "")
            {
                MessageBox.Show("Please enter a credit card number!");
                return;
            }
            //validation for phone number
            if(txtPhone.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid 10-digit phone number!");
                return;
            }

            int intPhoneNumber;
            if(!int.TryParse(txtPhone.Text,out intPhoneNumber))
            {
                MessageBox.Show("Phone number should only contain numbers!");
                return;
            }
            //validation for email
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Please enter an email address!");
                return;
            }
            //validation for gender
            if (cbbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an option for gender!");
                return;
            }
            
        }
    } 
}
    
