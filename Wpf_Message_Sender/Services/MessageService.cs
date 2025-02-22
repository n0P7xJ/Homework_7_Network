using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using WpfClient.Models;
using System.Net.Http;

namespace WpfClient.Services
{
    public class MessageService
    {
        private static string API = "uaa382b37c33b91cd4d2eb0de61a613c1e6620247e526ea92dfa18b850830f87c532b5";
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public MessageService() 
        { 
            _httpClient = new HttpClient();
            _url = "http://api.mobizon.ua";
        }

        public async Task<MessageResponse> SendMessage(MessagePostRequests messageToSend) 
        {
            string json = JsonConvert.SerializeObject(messageToSend, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_url}/service/message/sendSmsMessage?output=json&api=v1&apiKey={API}", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonResp = await response.Content.ReadAsStringAsync();
                if (jsonResp.Contains('['))
                {
                    throw new Exception("Invalid sending");
                }
                return JsonConvert.DeserializeObject<MessageResponse>(jsonResp);
            }

            throw new Exception("Invalid sending");
        }
    }
}
