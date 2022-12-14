using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Natech_Weather.Services
{
    public class NetworkService : INetworkService
    {
        private HttpClient _httpClient;
        public NetworkService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<TResult> GetAsync<TResult>(string url, CancellationToken ct)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url, ct);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);
            
            return result;
        }
    }
}
