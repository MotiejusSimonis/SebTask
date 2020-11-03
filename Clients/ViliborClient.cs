using SEBtask.Enums;
using SEBtask.Models.Dtos;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SEBtask.Clients
{
    public class ViliborClient : IViliborClient
    {
        private IHttpClientFactory _httpClientFactory;

        public ViliborClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ViliborDto> GetViliborRate(BaseRateCode baseRateCode)
        {
            var url = $"http://www.lb.lt/webservices/VilibidVilibor/VilibidVilibor.asmx/getLatestVilibRate?RateType={baseRateCode}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new ViliborDto
                {
                    IsSuccess = false,
                    BaseRateValue = null,
                };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var baseRateString = XDocument.Parse(responseString).Descendants().First(x => x.Name.LocalName == "decimal").Value;
            var baseRateValue = decimal.Parse(baseRateString.Replace(".", ","));

            return new ViliborDto
            {
                IsSuccess = true,
                BaseRateValue = baseRateValue,
            };
        }
    }


}
