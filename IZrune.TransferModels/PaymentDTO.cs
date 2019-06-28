using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
    public class PaymentDTO
    {
        public string url { get; set; }
        public string PurchaseDesc { get; set; }
        public int PurchaseAmt { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string MerchantName { get; set; }
        public string MerchantURL { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantID { get; set; }

        [JsonProperty("xDDDSProxy.Language")]
        public string Language { get; set; }
    }
}
