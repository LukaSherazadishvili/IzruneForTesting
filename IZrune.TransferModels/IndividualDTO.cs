using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
    public class IndividualDTO
    {
        public int months { get; set; }
        public int price { get; set; }
    }

    public class IndividualRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string promocode { get; set; }
        public List<IndividualDTO> prices { get; set; }
    }
}
