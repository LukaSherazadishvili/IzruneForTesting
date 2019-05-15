using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class RegionDTO
    {
        public string id { get; set; }
        public string title { get; set; }
        public List<SchoolDTO> schools { get; set; }
    }
}
