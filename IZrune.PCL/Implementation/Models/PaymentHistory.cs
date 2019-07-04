using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    class PaymentHistory : IPaymentHistory
    {
        public string StudentName { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
    }
}
