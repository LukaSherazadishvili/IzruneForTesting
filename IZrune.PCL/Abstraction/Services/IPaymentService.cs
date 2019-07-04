using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface IPaymentService
    {
        Task<IPay> GetPaymentUrlsAsync(int StudentId, int MonthCount, int Amount, string promoCode = "0");

        Task<IEnumerable<IPaymentHistory>> GetPaymentHistory();
        
    }
}
