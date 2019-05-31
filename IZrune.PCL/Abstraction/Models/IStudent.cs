using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IStudent
    {
        int id { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string PersonalNumber { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        int RegionId { get; set; }
        string Village { get; set; }
        DateTime Bdate { get; set; }
        int Class { get; set; }
        int SchoolId { get; set; }
        DateTime PackageStartDate { get; set; }
        int PackageMonthCount { get; set; }
    }
}
