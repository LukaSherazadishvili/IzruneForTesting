using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class QuisResultInfoRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public QuisResultInfoDTO info { get; set; }
    }
}
