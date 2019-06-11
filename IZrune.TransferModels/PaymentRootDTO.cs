using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
    public class PaymentRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public PaymentDTO payment { get; set; }
        public string payment_success_url { get; set; }
        public string payment_fail_url { get; set; }
    }
}
