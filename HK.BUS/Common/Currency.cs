using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK.BUS.Common
{
    public class Currency
    {
        public string Date { get; set; }

        public string CurrencyCode { get; set; }

        public int Unit { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyNameTR { get; set; }

        public decimal ForexBuying { get; set; }

        public decimal ForexSelling { get; set; }

        public decimal BanknoteBuying { get; set; }

        public decimal BanknoteSelling { get; set; }

        public decimal CrossRateUSD { get; set; }

        public decimal CrossRateOther { get; set; }
    }
    
    public class CurrencyModel
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string ConvertCurrency { get; set; }
    }

    public class BasicResponse
    {
        public BasicResponse()
        {
            IsSuccess = false;
            MessageList = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public List<string> MessageList { get; set; }
        public string Message { get; set; }
    }
}
