using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
                            AppCore.Instance.InitServices();
                            instance = new UserControl();
                        }
                    }
                }
                return instance;
            }
        }

        public IParent Parent;

        public IStudent CurrentStudent;

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
                if (Parent == null)
            Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();



            return Parent;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IStudent>> GetCurrentUserStudents()
        {
            try
            {
                if (Parent == null)
                {
                    Parent = await GetCurrentUser();
                }
                var Result = Parent?.Students;

                if (Result?.ToList().Count > 0)
                {
                    return Result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }

        public async void SeTSelectedStudent(int StudentId)
        {
            try
            {
                if (Parent == null)
                {
                    Parent = await GetCurrentUser();
                }

              
                    CurrentStudent = Parent?.Students.Where(i => i.id == StudentId)?.SingleOrDefault();
              
             
            }
            catch (Exception ex)
            {
              
            }
        }
        



   }
}
