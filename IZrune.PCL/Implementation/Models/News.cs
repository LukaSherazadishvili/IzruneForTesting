using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
   public class News : INews
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
