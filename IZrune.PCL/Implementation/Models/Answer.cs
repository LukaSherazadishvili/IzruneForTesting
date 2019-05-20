using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
   public class Answer : IAnswer
    {
        public string id { get; set; }
        public string title { get; set; }
        public string right { get; set; }
        public bool IsRight { get; set; }
    }
}
