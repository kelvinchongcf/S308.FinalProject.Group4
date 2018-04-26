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
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MemberInformation.xaml
    /// </summary>
    public partial class MemberInformation : Window
    {
        List<Members> memList, queryList;
        public MemberInformation()
        {
            InitializeComponent();
            memList = new List<Members>();
            queryList = new List<Members>();

            dtgMemInfo.ItemsSource = memList;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Validate last name
            int num;
            if (int.TryParse(txtLastNameSearch.Text, out num))
            {
                MessageBox.Show("Last name cannot be numeric.");
                return;
            }
            //Validate phone number
            if(txtPhoneSearch.Text.Length != 0)
            {
                if (txtPhoneSearch.Text.Length != 10)
                {
                    MessageBox.Show("Phone number is not valid.");
                    return;
                }
            }
            //Validate email
            if (txtEmailSearch.Text.Length != 0)
            {
                //Using functions to make sure it's a valid email format
                if (isValidEmail(txtEmailSearch.Text.Trim()) == false)
                {
                    MessageBox.Show("Email is invalid.");
                    return;
                }

            }
            string strFilePath = @"..\..\..\data.json";
            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();

                memList = JsonConvert.DeserializeObject<List<Members>>(jsonData);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Data import failed: " + ex.Message);
            }
            //Find matching data to criterias from the list
            //All criterias being used
            if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchLastName);
                queryList = queryList.FindAll(searchEmail);
                queryList = queryList.FindAll(searchPhone);
            }
            //Only last name as criteria
            else if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length == 0)
            {
                queryList = memList.FindAll(searchLastName);
            }
            //Only email as criteria
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length == 0)
            {
                queryList = memList.FindAll(searchEmail);
            }
            //Only use phone number as criteria
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchPhone);
            }
            //Use both email and phone as criteias
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchEmail);
                queryList = queryList.FindAll(searchPhone);
            }
            //Use both last name and phone as criterias
            else if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchLastName);
                queryList = queryList.FindAll(searchPhone);
            }
            //Use both last name and email as criterias
            else if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length == 0)
            {
                queryList = memList.FindAll(searchLastName);
                queryList = queryList.FindAll(searchEmail);
            }
            //If criteria is empty return everything in the list
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length == 0)
            {
                MessageBox.Show("No criterias were entered. Please input into one of the criteria above.");
                return;
            }
            bool isEmpty = !queryList.Any();
            if (isEmpty)
            {
                MessageBox.Show("No matching record.");
                return;
            }
            else {
                dtgMemInfo.ItemsSource = queryList;
                return;
            }
        }

        private void btnHomeFromPM_Click(object sender, RoutedEventArgs e)
        {
            //When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }
        //Find last name
        private bool searchLastName(Members m)
        {
            if (m.LastName == txtLastNameSearch.Text)
            {
                return true;
            }
            else
                return false;
        }
        //Find email
        private bool searchEmail(Members m)
        {
            if (m.Email == txtEmailSearch.Text)
            {
                return true;
            }
            else
                return false;
        }
        //find phone
        private bool searchPhone(Members m)
        {
            if (m.PhoneNo == Convert.ToDouble(txtPhoneSearch.Text))
            {
                return true;
            }
            else
                return false;
        }

        private void btnHomeFromPM_Click_1(object sender, RoutedEventArgs e)
        {//When clicked, navigate to destination page, closing the current page
            new MainMenu().Show();
            this.Close();
        }
    

        //function to check validity of email
        private bool isValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
