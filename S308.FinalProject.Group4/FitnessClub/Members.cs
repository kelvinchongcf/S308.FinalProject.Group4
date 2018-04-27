﻿using System;
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
        public double PhoneNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string PersonalGoals { get; set; }
        public string MembershipTypes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double MonthlyCost { get; set; }
        public double Total { get; set; }
        public bool PersonalTraining { get; set; }
        public bool LockerRental { get; set; }
    }
}
