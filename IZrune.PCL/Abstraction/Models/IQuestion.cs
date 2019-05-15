using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IQuestion
    {
         int id { get; set; }
         string title { get; set; }
         string image_url { get; set; }
         IEnumerable<string> images { get; set; }
         IEnumerable<IAnswer> Answers { get; set; }
    }
}
