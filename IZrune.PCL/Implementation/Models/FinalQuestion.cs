using System;
using System.Collections.Generic;
using System.Text;
using IZrune.PCL.Abstraction.Models;

namespace IZrune.PCL.Implementation.Models
{
    public class FinalQuestion : IFinalQuestion
    {
        public int StudentAnswerIndex { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string image_url { get; set; }
        public IEnumerable<string> images { get; set; }
        public IEnumerable<IAnswer> Answers { get; set; }
        public string Description { get; set; }
    }
}
