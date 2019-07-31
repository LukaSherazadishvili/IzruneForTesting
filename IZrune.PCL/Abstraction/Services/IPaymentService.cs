using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface IPaymentService
    {
        Task<IPay> GetPaymentUrlsAsync(IEnumerable<IStudent> Students, int PayBox = 0);

        Task<IEnumerable<IPaymentHistory>> GetPaymentHistory();

        Task<IPay> GetPaymentUrlsAsync(IStudent Student, int PayBox = 0);
    }
}
