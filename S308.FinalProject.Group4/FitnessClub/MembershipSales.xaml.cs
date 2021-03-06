﻿using System;
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

        private void ClearForm()
        {
            cbbMembershipType.SelectedIndex = -1;
            dpiStartDate.SelectedDate = DateTime.Today;
            cboPersonalTraining.IsChecked = false;
            cboLocker.IsChecked = false;
            lblShowMembership.Content = "";
            lblShowAvailability.Content = "";
            lblCalcCostPerMonth.Content = "";
            lblShowStartDateAnswer.Content = "";
            lblShowEndDateAnswer.Content = "";
            lblCalcSubtotal.Content = "";
            lblCalcAddCost.Content = "";
            lblCalcTotalCost.Content = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cbbCreditCardType.SelectedIndex = -1;
            txtCreditCardNumber.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            cbbGender.SelectedIndex = -1;
            txtAge.Text = "";
            txtWeight.Text = "";
            cbbPersonalGoal.SelectedIndex = -1;
            lblPersonalTraining.Content = "No";
            lblLockerRental.Content = "No";
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
            
            lblShowStartDateAnswer.Content = dtiStartDate.ToShortDateString();




            //Show the end date
            DateTime dtiEndDate;
            dtiEndDate = dtiStartDate.AddMonths(Convert.ToInt16(dblNumberOfMonths));
            lblShowEndDateAnswer.Content = dtiEndDate.ToShortDateString();
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
            string strGoal;
            string strGender;
           
            //Validation for inputs
            //Validation for first name
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter a first name!");
                return;
            }

            //validation for last name
            if (txtLastName.Text == "")
            {
                MessageBox.Show("Please enter a last name!");
                return;
            }

            //validation for credit card type
            if (cbbCreditCardType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a credit card type!");
                return;
            }
            //validation for credit card number
            if (txtCreditCardNumber.Text == "")
            {
                MessageBox.Show("Please enter a credit card number!");
                return;
            }
            //validation for phone number
            if (txtPhone.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid 10-digit phone number!");
                return;
            }

            long lngPhoneNumber;
            if (!Int64.TryParse(txtPhone.Text, out lngPhoneNumber))
            {
                MessageBox.Show("Phone number should only contain numbers!");
                return;
            }
            //validation for email
            if (txtEmail.Text == "")
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
            else
            {
                //show proper format gender
                int intGenderStartingPosition = cbbGender.SelectedItem.ToString().IndexOf(" ");
                strGender = cbbGender.SelectedItem.ToString().Substring(intGenderStartingPosition + 1);
            }

            if (cbbPersonalGoal.SelectedIndex != -1)
            {
                //show proper format fitness goal
                int intGoalStartingPosition = cbbPersonalGoal.SelectedItem.ToString().IndexOf(" ");
                strGoal = cbbPersonalGoal.SelectedItem.ToString().Substring(intGoalStartingPosition + 1);
            }
            else
                strGoal = "N/A";
           

            //Call AddMember method and passing all needed inputs
            //The method will return a bool type as the status of Add operation
            bolStatus = bolStatus = AddMember(Convert.ToString(lblShowMembership.Content), dpiStartDate.Text, Convert.ToString(lblShowEndDateAnswer.Content), Convert.ToString(lblCalcCostPerMonth.Content), Convert.ToString(lblCalcSubtotal.Content), Convert.ToString(lblPersonalTraining.Content), Convert.ToString(lblLockerRental.Content), Convert.ToString(lblCalcTotalCost.Content), txtFirstName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text, strGender , txtAge.Text, txtWeight.Text, strGoal);

            //If the member is added successfully, display message to user
            if (bolStatus)
            {
                MessageBox.Show("You now have " + MemberList.Count + " members in the system");
            }

        }

        private bool AddMember(string mType, string sDate, string eDate, string CostpMonth, string subtotal, string personalt, string locker, string total, string firstName, string lastName, string phone, string email, string gender, string age, string weight, object goal)
        {
            //Define variables
            Member newMember;
            string strGoal;

            //show proper gender
            int intGenderStartingPosition = cbbGender.SelectedItem.ToString().IndexOf(" ");
            string strGender = cbbGender.SelectedItem.ToString().Substring(intGenderStartingPosition + 1);

            if (cbbPersonalGoal.SelectedIndex != -1)
            {
                //show proper format fitness goal
                int intGoalStartingPosition = cbbPersonalGoal.SelectedItem.ToString().IndexOf(" ");
                strGoal = cbbPersonalGoal.SelectedItem.ToString().Substring(intGoalStartingPosition + 1);
            }
            else
                strGoal = "";


            //if Age is not entered
            string strAge = txtAge.Text;
                     string strWeight = txtWeight.Text;

                     if (strAge == "" && strWeight == "" && goal == null)
                     {
                         newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, "N/A", "N/A", "N/A");
                     }
                     else if(strAge == "" && strWeight =="")
                     {
                         newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, "N/A", "N/A", strGoal);
                     }
                     else if(strAge == "" && goal == null)
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, "N/A", weight, "N/A");
            }
                     else if(strWeight == "" && goal == null)
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, age, "N/A", "N/A");
            }
                     else if(strAge == "")
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, "N/A", weight, strGoal);
            }
                     else if(strWeight == "")
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, age, "N/A", strGoal);
            }
                     else if(goal == null)
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, age, weight, "N/A");
            }
                     else
            {
                newMember = new Member(mType, sDate, eDate, CostpMonth, subtotal, personalt, locker, total, firstName, lastName, phone, email, strGender, age, weight, strGoal);
            }

            if (Convert.ToString(lblShowAvailability.Content) == "No")
            {
                MessageBox.Show("The selected membership type is current unavailable, please select another membership!");
                return false;
            }
            string strFilePath = @"..\..\..\data.json";
            MemberList.Add(newMember);

            try
            {
                string jsonData = JsonConvert.SerializeObject(MemberList);
                System.IO.File.WriteAllText(strFilePath, jsonData);
                MessageBox.Show("Member successfully added!" + Environment.NewLine + 
                    "Membership Type: " + lblShowMembership.Content + Environment.NewLine +
                    "Membership Cost Per Month: " + lblCalcCostPerMonth.Content + Environment.NewLine +
                    "Start Date: " + lblShowStartDateAnswer.Content + Environment.NewLine + 
                    "End Date: " + lblShowEndDateAnswer.Content + Environment.NewLine + 
                    "Subtotal: " + lblShowMembership.Content + Environment.NewLine +
                    "Personal Training Plan: " + lblPersonalTraining.Content + Environment.NewLine +
                    "Locker Rental: " + lblLockerRental.Content + Environment.NewLine +
                    "Total Cost: " + lblCalcTotalCost.Content + Environment.NewLine +
                    "First Name: " + txtFirstName.Text + Environment.NewLine +
                    "Last Name: " + txtLastName.Text + Environment.NewLine +
                    "Phone: " + txtPhone.Text + Environment.NewLine +
                    "Email: " + txtEmail.Text + Environment.NewLine +
                    "Gender: " + strGender + Environment.NewLine +
                    "Age: " + txtAge.Text + Environment.NewLine +
                    "Weight: " + txtWeight.Text + Environment.NewLine +
                    "Fitness Goal: " +strGoal + Environment.NewLine
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in adding member");
            }

            ClearForm();

            return true;
      }
          
        }






    }


