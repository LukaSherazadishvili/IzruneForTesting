using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class QuestionRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string test_id { get; set; }
        public List<QuestionDTO> questions { get; set; }
    }
}
