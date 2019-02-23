using HK.BUS.Common;
using HK.BUS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HK.BUS.Services
{
    public class DataService : BaseService
    {
        internal DataService(IApplicationService appService) : base(appService)
        {
        }

        private List<Currency> GetXmlDataCurrencyList()
        {
            List<Currency> exchangeRateList = null;
            string url = "http://www.tcmb.gov.tr/kurlar/today.xml";
            try
            {
                XDocument xmlDoc = XDocument.Load(url);
                if (xmlDoc != null)
                {

                    exchangeRateList = (from p in xmlDoc.Element("Tarih_Date").Elements("Currency")
                                        select new Currency()
                                        {
                                            Date = (string)xmlDoc.Element("Tarih_Date").Attribute("Tarih"),
                                            CurrencyCode = (string)p.Attribute("CurrencyCode"),
                                            CurrencyNameTR = (string)p.Element("Isim"),
                                            CurrencyName = (string)p.Element("CurrencyName"),
                                            ForexBuying = Convert.ToDecimal((String.IsNullOrEmpty((string)p.Element("ForexBuying")) ? "0" : (string)p.Element("ForexBuying")).Replace(".", ",")),
                                            ForexSelling = Convert.ToDecimal((String.IsNullOrEmpty((string)p.Element("ForexSelling")) ? "0" : (string)p.Element("ForexSelling")).Replace(".", ",")),
                                            BanknoteBuying = Convert.ToDecimal((String.IsNullOrEmpty((string)p.Element("BanknoteBuying")) ? "0" : (string)p.Element("BanknoteBuying")).Replace(".", ",")),
                                            BanknoteSelling = Convert.ToDecimal((String.IsNullOrEmpty((string)p.Element("BanknoteSelling")) ? "0" : (string)p.Element("BanknoteSelling")).Replace(".", ",")),
                                            Unit = Convert.ToInt32((string)p.Element("Unit")),
                                            CrossRateOther = Convert.ToDecimal((String.IsNullOrEmpty((string)p.Element("CrossRateOther")) ? "0" : (string)p.Element("CrossRateOther")).Replace(".", ",")),
                                            CrossRateUSD = Convert.ToDecimal((String.IsNullOrEmpty((string)p.Element("CrossRateUSD")) ? "0" : (string)p.Element("CrossRateUSD")).Replace(".", ","))
                                        }).ToList();

                    return exchangeRateList;
                }
                else
                {
                    exchangeRateList = new List<Currency>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return exchangeRateList;
        }

        public List<string> GetCurrencyNameList()
        {
            List<string> list = new List<string>();
            list.Add("TRY");
            list.AddRange(GetXmlDataCurrencyList().Select(x => x.CurrencyCode).ToList());

            return list;
        }

        public BasicResponse CalculateCurrency(CurrencyModel model)
        {
            BasicResponse response = new BasicResponse();
            decimal convertedAmount = 0;

            List<Currency> data = GetXmlDataCurrencyList();
            try
            {
                if (model.Currency == model.ConvertCurrency)
                {
                    convertedAmount = model.Amount;
                }
                else if (model.Currency == "TRY")
                {
                    convertedAmount = model.Amount * (1 / (data.Find(x => x.CurrencyCode == model.ConvertCurrency).ForexBuying / data.Find(x => x.CurrencyCode == model.ConvertCurrency).Unit));
                }
                else if (model.ConvertCurrency == "TRY")
                {
                    convertedAmount = model.Amount * data.Find(x => x.CurrencyCode == model.Currency).ForexBuying;
                }
                else
                {
                    convertedAmount = model.Amount * ((data.Find(x => x.CurrencyCode == model.Currency).ForexBuying / data.Find(x => x.CurrencyCode == model.Currency).Unit) / 
                                                      (data.Find(x => x.CurrencyCode == model.ConvertCurrency).ForexBuying / data.Find(x => x.CurrencyCode == model.ConvertCurrency).Unit));
                }
                convertedAmount = decimal.Round(convertedAmount, 4);

                response.Message = String.Format($"{model.Amount} {model.Currency} <<=>> {convertedAmount} {model.ConvertCurrency}");
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
