using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IPay
    {
        string CurrentUserPayURl { get; set; }
        string SuccesUrl { get; set; }
        string FailUrl { get; set; }
    }
}
