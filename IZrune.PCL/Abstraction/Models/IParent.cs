using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IParent
    {
        int id { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string City { get; set; }
        string Vilage { get; set; }
        DateTime? bDate { get; set; }
        IEnumerable<IStudent> Students { get; set; }
    }
}
