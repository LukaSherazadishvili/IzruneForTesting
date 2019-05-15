using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class RegionRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<RegionDTO> regions { get; set; }
    }
}
