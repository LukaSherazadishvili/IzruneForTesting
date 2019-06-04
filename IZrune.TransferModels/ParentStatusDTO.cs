        using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IZrune.TransferModels
{
   public class ParentStatusDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string ID { get; set; }
        public string profile_number { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string village { get; set; }
        public string birth_date { get; set; }

        [JsonProperty("SMS code")]
        public string SmsCode { get; set; }

        public List<StudentStatusDTO> students { get; set; }
    }
}
