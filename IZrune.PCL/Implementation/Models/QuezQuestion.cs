using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class QuezQuestion : IQuezQuestion
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Duration { get; set; }
    }
}
