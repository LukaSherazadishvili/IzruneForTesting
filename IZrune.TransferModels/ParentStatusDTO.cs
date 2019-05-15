        using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class ParentStatusDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string ID { get; set; }
        public string profile_number { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public List<StudentStatusDTO> students { get; set; }
    }
}
