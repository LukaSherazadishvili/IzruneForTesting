using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class QuisInfo : IQuisInfo
    {
        public IQuisResultInfo QueisResult { get; set; }
    }
}
