using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
   public class TestController : ITestControler
    {
        public int Duration { get; set; }
        public IEnumerable<IQuezQuestion> Questions { get; set; }
    }
}
