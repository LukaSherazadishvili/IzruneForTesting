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

        Task<bool> EditParentProfileAsync(string ParrentMail, string ParrentPhone, string City, string Village);

     
        Task EditStudentProfile(string Email, string Phone, int regionId, string village, int SchoolId);
      
    }
}
