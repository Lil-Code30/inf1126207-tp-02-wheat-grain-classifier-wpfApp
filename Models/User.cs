using Newtonsoft.Json;

namespace WheatGrainClassifierWpfApp.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }

    public class UserResponse
    {
        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }
}
