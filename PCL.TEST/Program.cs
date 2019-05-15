using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Implementation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.TEST
{
    class Program
    {
        static async void Main(string[] args)
        {
            LoginServices serv = new LoginServices();
            var result = await serv.LoginUser("irakli123", "123456789");

            Console.ReadKey();
        }
    }
}
