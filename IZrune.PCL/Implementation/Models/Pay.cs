using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class Pay : IPay
    {
        public string CurrentUserPayURl { get; set; }
        public string SuccesUrl { get; set; }
        public string FailUrl { get; set; }
    }
}
