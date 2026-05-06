using Newtonsoft.Json;
using System.Buffers.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Services
{
    public class ApiService
    {
        private HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://dummyjson.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<List<User>> GetUsersAsync(string path, int limit = 10)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{path}?limit={limit}");

            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(data);
                return users;
            }

            return new List<User>();
        }

        public List<User> GetUsers(string path)
        {
            List<User> users = new List<User>();

            try
            {
                Task<List<User>> task = Task.Run(async () => await GetUsersAsync(path));
                task.Wait();
                users = task.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return users;
        }


    }
}
