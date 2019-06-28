using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.TransferModels
{
   public class PaymentHistoryDTO
    {
        public string student_id { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
    }

    public class PaymentHistoryRootDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<PaymentHistoryDTO> payment_history { get; set; }
    }
}
