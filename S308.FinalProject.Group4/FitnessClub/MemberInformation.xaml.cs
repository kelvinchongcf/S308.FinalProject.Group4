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
            string strFilePath = "data.json";
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
            if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchLastName);
                queryList = queryList.FindAll(searchEmail);
                queryList = queryList.FindAll(searchPhone);
            }
            else if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length == 0)
            {
                queryList = memList.FindAll(searchLastName);
            }
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length == 0)
            {
                queryList = memList.FindAll(searchEmail);
            }
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchPhone);
            }
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchEmail);
                queryList = queryList.FindAll(searchPhone);
            }
            else if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length != 0)
            {
                queryList = memList.FindAll(searchLastName);
                queryList = queryList.FindAll(searchPhone);
            }
            else if (txtLastNameSearch.Text.Length != 0 && txtEmailSearch.Text.Length != 0 && txtPhoneSearch.Text.Length == 0)
            {
                queryList = memList.FindAll(searchLastName);
                queryList = queryList.FindAll(searchEmail);
            }
            else if (txtLastNameSearch.Text.Length == 0 && txtEmailSearch.Text.Length == 0 && txtPhoneSearch.Text.Length == 0)
            {
                MessageBox.Show("Criteria(s) can't be empty");
                return;
            }
            dtgMemInfo.ItemsSource = queryList;
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
            if (m.PhoneNo == Convert.ToInt32(txtPhoneSearch.Text))
            {
                return true;
            }
            else
                return false;
        }

    }
}
