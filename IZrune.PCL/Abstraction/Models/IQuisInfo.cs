using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IQuisInfo
    {
         string DiplomaURl { get; set; }
        IQuisResultInfo QueisResult { set; get; }
    }
}
