using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FitnessClub
{
    public class Members
    {
        private string v1;
        private string v2;
        private object content1;
        private object content2;
        private CheckBox cboPersonalTraining;
        private CheckBox cboLocker;
        private object content3;
        private string text1;
        private string text2;
        private int v3;
        private string text3;
        private string v4;
        private int v5;
        private double v6;
        private string v7;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCardType { get; set; }
        public long CreditCardNumber { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string PersonalGoals { get; set; }
        public string MembershipTypes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double MonthlyCost { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public CheckBox PersonalTraining { get; set; }
        public CheckBox LockerRental { get; set; }

        public Members(string membershipType, DateTime startDate, double costperMonth, double subTotal, CheckBox personalTrain, CheckBox addLocker, double total, string firstName, string lastName, int phone, string email, string gender, int age, double weight, string goal)
        {
            MembershipTypes = membershipType;
            StartDate = startDate;
            MonthlyCost = costperMonth;
            Subtotal = subTotal;
            PersonalTraining = personalTrain;
            LockerRental = addLocker;
            Total = total;
            FirstName = firstName;
            LastName = lastName;
            PhoneNo = phone;
            Email = email;
            Gender = gender;
            Age = age;
            Weight = weight;
            PersonalGoals = goal;



        }

        public Members(string v1, string v2, object content1, object content2, CheckBox cboPersonalTraining, CheckBox cboLocker, object content3, string text1, string text2, int v3, string text3, string v4, int v5, double v6, string v7)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.content1 = content1;
            this.content2 = content2;
            this.cboPersonalTraining = cboPersonalTraining;
            this.cboLocker = cboLocker;
            this.content3 = content3;
            this.text1 = text1;
            this.text2 = text2;
            this.v3 = v3;
            this.text3 = text3;
            this.v4 = v4;
            this.v5 = v5;
            this.v6 = v6;
            this.v7 = v7;
        }
    }

}
