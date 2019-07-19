using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
  public  class Question : IQuestion
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image_url { get; set; }
        public IEnumerable<string> images { get; set; }
        public IEnumerable<IAnswer> Answers { get; set; }
        public string Description { get; set; }
    }
}
