using DeliverySystem.Services;

namespace maxim_technology_task2.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly HttpClient _client;

        public RandomNumberService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetRandomNumber()
        {
            var numbers = await _client.GetFromJsonAsync<List<int>>(
                "https://www.randomnumberapi.com/api/v1.0/random"
            );

            return numbers![0];
        }
    }
}
