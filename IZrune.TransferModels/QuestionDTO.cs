using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class img
    {
        public string url { get; set; }
    }
   public class QuestionDTO
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image_url { get; set; }
        public List<img> images { get; set; }
        public List<AnswerDTO> answers { get; set; }
    }
}
