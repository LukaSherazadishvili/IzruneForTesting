using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IZrune.TransferModels
{
   public class StudentStatusDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string personal_number { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string region_id { get; set; }
        public string city { get; set; }
        public string village { get; set; }
        public string birth_date { get; set; }
        public string school_id { get; set; }
        public string school { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("expiration_date")]
        public string PackageEndDate { get; set; }
    }
}
