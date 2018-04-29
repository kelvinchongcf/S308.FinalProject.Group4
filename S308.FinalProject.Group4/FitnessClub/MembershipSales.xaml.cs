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
        string strFilePath = @"..\..\..\MembershipPricing.json";
        //define lists to contain subitems
        List<Pricing> TypeList;
        List<Member> MemberList;
        
        public MembershipSales()
        {
            InitializeComponent();
            //create a list to store membership type data
            TypeList = new List<Pricing>();
            //create a list to store member data
            MemberList = new List<Member>();
            //make membership combo box default to empty
            cbbMembershipType.SelectedIndex = -1;
            cboPersonalTraining.IsChecked = false;
            cboLocker.IsChecked = false;
            ImportMemberData();

            

            

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

        //read membership pricing json file
        private void OpenPricingJson(string FilePath)
        {
            string jsonData = File.ReadAllText(FilePath);
            TypeList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);
        }
        //import member list
        private void ImportMemberData()
        {
            string strFilePath = @"..\..\..\data.json";
            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                MemberList = JsonConvert.DeserializeObject<List<Member>>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in importing members' data!");
            }
        }
 
        

        private void btnHomeFromPM_Click(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }

        private void btnQuote_Click(object sender, RoutedEventArgs e)
        { //Validation membership type is required
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
            //assign values to strMemberType as users click different membership
            string strMemberType = "";
            ComboBoxItem cboMembershipSelected = (ComboBoxItem)cbbMembershipType.SelectedItem;
            strMemberType = cboMembershipSelected.Content.ToString();
            //open json file
            OpenPricingJson(strFilePath);
            //assgin availability and cost per month
            foreach (Pricing P in TypeList)
            {
                if(P.MembershipType == strMemberType)
                {
                    lblShowMembership.Content = P.MembershipType;
                    lblShowAvailability.Content = P.Availability;
                    lblCalcCostPerMonth.Content = P.Price;
                }
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

            //change cost per month from text to double
            if (!double.TryParse(Convert.ToString(lblCalcCostPerMonth.Content), out dblMembershipCostPerMonth))
            {
                MessageBox.Show("Error");
                return;
            }

            //Months to use
            if (cbbMembershipType.SelectedIndex == 0 || cbbMembershipType.SelectedIndex == 2 || cbbMembershipType.SelectedIndex == 4)
            {
                dblNumberOfMonths = 1;
            }
            else dblNumberOfMonths = 12;


            

            //Show the start date user selected
            lblShowStartDateAnswer.Content = dpiStartDate.SelectedDate;


           

            //Show the end date
            lblShowEndDateAnswer.Content = dtiStartDate.AddMonths(Convert.ToInt16(dblNumberOfMonths));

            //Calculating Subtotal

            lblCalcSubtotal.Content = (dblMembershipCostPerMonth * dblNumberOfMonths).ToString("c2");

            //Calculate Additional Cost
            if (cboPersonalTraining.IsChecked == true)
            {
                dblAddPTP = 5.00;
                lblPersonalTraining.Content = "Yes";
            }
            else
            {
                dblAddPTP = 0;
                lblPersonalTraining.Content = "No";
            }

            if (cboLocker.IsChecked == true)
            {
                dblAddLR = 1.00;
                lblLockerRental.Content = "Yes";
            }
            else
            {
                dblAddLR = 0;
                lblLockerRental.Content = "No";
            }

            lblCalcAddCost.Content = ((dblAddPTP + dblAddLR) * dblNumberOfMonths).ToString("c2");

            //Calculate Total Cost
            lblCalcTotalCost.Content = (((dblMembershipCostPerMonth * dblNumberOfMonths) + ((dblAddPTP + dblAddLR) * dblNumberOfMonths)).ToString("c2"));

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
        { //Define variables
            bool bolStatus;

            //Call AddMember method and passing all needed inputs
            //The method will return a bool type as the status of Add operation
            bolStatus = bolStatus = AddMember(Convert.ToString(lblShowMembership.Content), dpiStartDate.Text, Convert.ToString(lblShowEndDateAnswer.Content), Convert.ToString(lblCalcCostPerMonth.Content), Convert.ToString(lblCalcSubtotal.Content), Convert.ToString(lblPersonalTraining.Content), Convert.ToString(lblLockerRental.Content), Convert.ToString(lblCalcTotalCost.Content), txtFirstName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text, cbbGender.SelectedItem , txtAge.Text, txtWeight.Text, cbbPersonalGoal.SelectedItem);

            //If the member is added successfully, display message to user
            if (bolStatus)
            {
                MessageBox.Show("You now have " + MemberList.Count + " members in the system");
            }

        }

        private bool AddMember(string mType, string sDate, string eDate, string CostpMonth, string subtotal, string personalt, string locker, string total, string firstName, string lastName, string phone, string email, object gender, string age, string weight, object goal)
        {
            //Define variables
            Member newMember;

            //Validation for inputs
            //Validation for first name
            if(txtFirstName.Text == "")
            {
            MessageBox.Show("Please enter a first name!");
            return false;
            }

            //validation for last name
            if(txtLastName.Text == "")
            {
            MessageBox.Show("Please enter a last name!");
            return false;
            }

            //validation for credit card type
            if(cbbCreditCardType.SelectedIndex == -1)
            {
            MessageBox.Show("Please select a credit card type!");
            return false;
            }
            //validation for credit card number
            if(txtCreditCardNumber.Text == "")
            {
            MessageBox.Show("Please enter a credit card number!");
            return false;
            }
            //validation for phone number
            if(txtPhone.Text.Length != 10)
            {
            MessageBox.Show("Please enter a valid 10-digit phone number!");
            return false;
            }

            int intPhoneNumber;
            if(!int.TryParse(txtPhone.Text,out intPhoneNumber))
            {
            MessageBox.Show("Phone number should only contain numbers!");
            return false;
            }
            //validation for email
            if(txtEmail.Text == "")
            {
            MessageBox.Show("Please enter an email address!");
            return false;
            }
            //validation for gender
            if (cbbGender.SelectedIndex == -1)
            {
            MessageBox.Show("Please select an option for gender!");
            return false;
            }

            //validate to not allow user to sell unavailable membership
            string strAvailability = lblShowAvailability.Content.ToString();
            if(strAvailability == "No")
            {
                MessageBox.Show("The selected membershiptype is not available, please select another membership!");
            }

            //if Age is not entered
            string strAge = txtAge.Text;
            if(strAge == "")
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, gender.ToString(), "N/A", weight, goal.ToString());
            }
            else
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, gender.ToString(), age, weight, goal.ToString());
            }

            //if weight is not entered
            string strWeight = txtWeight.Text;
            if(strWeight == "")
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, gender.ToString(), age, "N/A", goal.ToString());
            }
            else
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, gender.ToString(), age, weight, goal.ToString());
            }
            

            //if goal is not entered
            if(goal == null)
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, gender.ToString(), age, weight, "N/A");
            }
            else
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, gender.ToString(), age, weight, goal.ToString());
            }

            string strFilePath = @"..\..\..\data.json";
            MemberList.Add(newMember);

            try
            {
                string jsonData = JsonConvert.SerializeObject(MemberList);
                System.IO.File.WriteAllText(strFilePath, jsonData);
                MessageBox.Show("Member successfully added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in adding member");
            }
            return true;
      }
          /* 
            //Do validations on input here! (NOT YET DONE)
            //Load text file into list
            string strFilePath1 = @"..\..\..\data.json";

            try
            {
                StreamReader reader = new StreamReader(strFilePath1);
                string jsonData = reader.ReadToEnd();
                reader.Close();

                memList = JsonConvert.DeserializeObject<List<Members>>(jsonData);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data import failed: " + ex.Message);
            }
            //Create a new class for new member ( NOT FINISHED ) Add more into the variables I don't have access to. (Willy)
            Members NewMem = new Members(cbbMembershipType.SelectedItem.ToString(), dpiStartDate.SelectedDate, Convert.ToDouble(lblCalcCostPerMonth.Content),
                Convert.ToDouble(lblCalcSubtotal.Content),cboPersonalTraining,cboLocker, Convert.ToDouble(lblCalcTotalCost.Content), txtFirstName.Text, txtLastName.Text,
                Convert.ToInt32(txtPhone.Text), txtEmail.Text, cbbGender.SelectedIndex.ToString(),
                Convert.ToInt32(txtAge.Text), Convert.ToDouble(txtWeight.Text), cbbPersonalGoal.SelectedIndex.ToString());

            // add it to list of members
            memList.Add(NewMem);
            //Overwrite json file with new list

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath1, false);
                string jsonData = JsonConvert.SerializeObject(memList);
                writer.Write(jsonData);
                writer.Close();
            }
            //send error message if error occurs
            catch (Exception ex)
            {
                MessageBox.Show("Export Failed: " + ex.Message);
            }
            //Senbd notification of saved file and the filepath of new file
            MessageBox.Show("Export is successful." + Environment.NewLine + "File Created: " + strFilePath1);

*/
        }






    }


