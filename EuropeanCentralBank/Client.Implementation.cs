using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EuropeanCentralBank.Helpers;
using Microsoft.Extensions.Options;

namespace EuropeanCentralBank
{
    internal class EuropeanCentralBankClient : IEuropeanCentralBankClient
    {
        private readonly EuropeanCentralBankSettings _settings;
        private readonly XNamespace nameSpace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

        public EuropeanCentralBankClient(IOptions<EuropeanCentralBankSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<RatesResponse> GetRates()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(_settings.RatesEndpoint);

                    var xdoc = await response.Content.ReadAsStringAsync();

                    RatesResponse ratesResponse = ParseXmlDocumentToRatesResponse(xdoc);

                    return ratesResponse;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private RatesResponse ParseXmlDocumentToRatesResponse(string xdoc)
        {
            var doc = XDocument.Parse(xdoc);

            var currencyRates = doc.Root
                            .Element(nameSpace + "Cube")
                            .Element(nameSpace + "Cube")
                            .Elements(nameSpace + "Cube")
                            .Select(e => new CurrencyRate(e.Attribute("currency").Value, (decimal)e.Attribute("rate"))).ToList().AsReadOnly();

            var dateTimeString = doc.Root
                            .Element(nameSpace + "Cube")
                            .Element(nameSpace + "Cube").Attribute("time").Value;
            XmlRootAttribute xRoot = new XmlRootAttribute("Cube");

            var dateTime = Convert.ToDateTime(dateTimeString);

            var ratesResponse = new RatesResponse(dateTime, currencyRates);
            return ratesResponse;
        }
    }

}
