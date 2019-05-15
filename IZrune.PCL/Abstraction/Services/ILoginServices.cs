using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface ILoginServices
    {
        Task<bool> LoginUser(string username, string password);
    }
}
