using BlazorSignalRClient.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorSignalRClient.Services
{
    public class ApiService
    {
        public HttpClient _httpClient;
        public ApiService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<string> fetchdata()
        {
            var response =  _httpClient.GetAsync("mytest/getmsg");
            var responseContent = await response.Result.Content.ReadAsStreamAsync();
            return JsonSerializer.DeserializeAsync<Mydata>(responseContent).Result.myname;
        }

        public async Task<Mydata> GetMyData()
        {
            var response = await _httpClient.GetAsync("api/mytest");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Mydata>(responseContent);
        }

    }
}