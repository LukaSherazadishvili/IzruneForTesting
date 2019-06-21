using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface IRegistrationServices
    {
        Task<IEnumerable<IRegion>> GetRegionsAsync();
        Task<IPay> RegistrationUser(IParent user,IEnumerable<IStudent> student);

        Task<string> GetAgreement();

    }
}
