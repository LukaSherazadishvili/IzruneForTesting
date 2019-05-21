using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MpdcContainer = ServiceContainer.ServiceContainer;


namespace IZrune.PCL.Helpers
{
   public class UserControl
    {
        private static UserControl instance = null;
        private static readonly object padlock = new object();

        UserControl()
        {
        }

        public static UserControl Instance {
            get {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new UserControl();
                        }
                    }
                }
                return instance;
            }
        }

        private IParent Parent;

        public async Task<bool>  IsLogedIn()
        {
            try
            {
                var result = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();
                if (result == null)
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool>LogInUser(string UserName,string Password)
        {
            try
            {
                var Result = await MpdcContainer.Instance.Get<ILoginServices>().LoginUser(UserName, Password);

                return Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IParent> GetCurrentUser()
        {
            try { 
            Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();

            return Parent;

            }
            catch(Exception ex)
            {
                return null;
            }
        }


    }
}
