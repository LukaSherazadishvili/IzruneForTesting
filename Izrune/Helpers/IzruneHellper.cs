using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IZrune.PCL.Abstraction.Models;

namespace Izrune.Helpers
{
   public class IzruneHellper
    {
        private static IzruneHellper instance = null;
        private static readonly object padlock = new object();

        IzruneHellper()
        {
        }

        public static IzruneHellper Instance {
            get {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IzruneHellper();
                    }
                    return instance;
                }
            }
        }

        public INews CurrentNews { get; set; }

        public IStudentsStatistic CurrentStatistic { get; set; }

       public List<string> Monthes = new List<string>
            {
           "თვე",
                "იანვარი",
                "თებერვალი",
                "მარტი",
                "აპრილი",
                "მაისი",
                "ივნისი",
                "ივლისი",
                "აგვისტო",
                "სექტემბერი",
                "ოქტომბერი",
                "ნოემვერი",
                "დეკემბერი",
            };


        public void SaveUserNamePassword(string UserName,string Password)
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.PutString("UserName", UserName);
            edit.PutString("Password", Password);
            edit.Apply();
        }

        public int CurrentStudentAmount=0;

        public List<string> letters = new List<string>()
        {
            "ა","ბ","გ","დ","ე","ვ","ზ","თ","ი","კ","ლ","მ","ნ","ო","პ","ჟ","რ","ს","ტ","უ","ფ","ქ","ღ","შ","ც","ძ","წ","ჭ","ხ","ჯ","ჰ"
        };

    }
}