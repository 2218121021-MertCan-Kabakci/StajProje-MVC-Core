using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ElsaYazilim.Helpers
{
    public class Service
    {
        string BaseUrl = "https://localhost:44322/api/";
        public async Task<string> GetMethodWithReturn(string apiUrlWithParameter)
        {

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(20);
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.GetAsync(apiUrlWithParameter);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();

                }
                else return null;
            }

        }

        public async Task<string> PostMethodWithReturn(object dto, string apiMethod, string token)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(BaseUrl);
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiMethod, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else return null;
            }
        }
    }
}
