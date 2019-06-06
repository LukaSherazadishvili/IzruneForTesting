using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class Region : IRegion
    {
        public int id { get; set; }
        public string title { get; set; }
        public IEnumerable<ISchool> Schools { get; set; }
    }
}
