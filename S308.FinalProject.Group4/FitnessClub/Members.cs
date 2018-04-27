using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Members
    {
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
        public bool PersonalTraining { get; set; }
        public bool LockerRental { get; set; }

        public Members(string membershipType, DateTime startDate, double costperMonth, double subTotal, bool personalTrain, bool addLocker, double total, string firstName, string lastName, int phone, string email, string gender, int age, double weight, string goal)
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
        

        
    }

}
