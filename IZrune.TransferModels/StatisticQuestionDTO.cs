using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class StatisticQuestionDTO
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image_url { get; set; }
        public List<img> images { get; set; }
        public List<StatisticAnswersDTO> answers { get; set; }
        public string description { get; set; }
        public string source { get; set; }
    }
}
