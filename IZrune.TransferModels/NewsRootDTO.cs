using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class NewsRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<NewsDTO> news { get; set; }
    }
}
