using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IQuezQuestion
    {
         int QuestionId { get; set; }
         int AnswerId { get; set; }
    }
}
