using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    class School : ISchool
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
