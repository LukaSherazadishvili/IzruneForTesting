using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
  public  interface INewsService
    {
        Task<IEnumerable<INews>> GetNewsAsync();

        Task<string> GetMoreInfoAsync();
    }
}
