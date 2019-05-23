using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface INews
    {
        string Title { get; set; }
        string ImageUrl { get; set; }
        DateTime date { get; set; }
        string Category { get; set; }
        string Description { get; set; }
        string Content { get; set; }
    }
}
