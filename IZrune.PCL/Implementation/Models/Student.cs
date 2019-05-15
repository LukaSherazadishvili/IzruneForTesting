using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class Student : IStudent
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RegionId { get; set; }
        public string Village { get; set; }
        public DateTime? Bdate { get; set; }
        public int? SchoolId { get; set; }
        public DateTime? PackageStartDate { get; set; }
        public int? PackageMonthCount { get; set; }
        public int? Class { get; set; }
    }
}
