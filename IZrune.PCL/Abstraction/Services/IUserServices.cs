using IZrune.PCL.Abstraction.Models;
using IZrune.TransferModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface IUserServices
    {
        Task<IParent> GetUserAsync();

        Task<IPromoCode> GetPromoCodeAsync(string SchoolId);


    }
}
