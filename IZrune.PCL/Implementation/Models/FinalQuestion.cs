using System;
using System.Collections.Generic;
using System.Text;
using IZrune.PCL.Abstraction.Models;

namespace IZrune.PCL.Implementation.Models
{
    public class FinalQuestion : IFinalQuestion
    {
        public string Title { get; set; }
        
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<IFinalAnswers> Answers { get; set; }
    }
}
