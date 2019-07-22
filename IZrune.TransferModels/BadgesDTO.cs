using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class BadgesDTO
    {
        public string url { get; set; }
        public string title { get; set; }

    }

    public class BadgesRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<BadgesDTO> badges { get; set; }

    }
}
