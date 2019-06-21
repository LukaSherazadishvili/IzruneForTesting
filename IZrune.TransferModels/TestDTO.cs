using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class TestDTO
    {
        public string test_id { get; set; }
        public string test_type { get; set; }
        public string date { get; set; }
        public string score { get; set; }
        public string duration { get; set; }
        public string egmu { get; set; }
        public string diploma_url { get; set; }
        public List<StatisticQuestionDTO> questions { get; set; }
    }
}
