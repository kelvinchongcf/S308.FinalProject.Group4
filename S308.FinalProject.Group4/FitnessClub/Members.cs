using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FitnessClub
{
    public class Member
    {
        public string MembershipType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CostPerMonth { get; set; }
        public string Subtotal { get; set; }
        public string PersonalTraining { get; set; }
        public string Locker { get; set; }
        public string Total { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string FitnessGoal { get; set; }

        public Member()
        {

        }

        //assign initial values
        public Member(string mType, string sDate, string eDate, string CostpMonth, string subtotal, string personalt, string locker, string total, string firstName, string lastName, string phone, string email, string gender, string age, string weight, string goal)
        {
            MembershipType = mType;
            StartDate = sDate;
            EndDate = eDate;
            CostPerMonth = CostpMonth;
            Subtotal = subtotal;
            PersonalTraining = personalt;
            Locker = locker;
            Total = total;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Gender = gender;
            Weight = weight;
            FitnessGoal = goal;



        }
    }
}
