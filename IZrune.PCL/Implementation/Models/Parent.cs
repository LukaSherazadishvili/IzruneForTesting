using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class Parent : IParent
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Vilage { get; set; }
        public DateTime? bDate { get; set; }
        public IEnumerable<IStudent> Students { get; set; }
        public int ProfileNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}
