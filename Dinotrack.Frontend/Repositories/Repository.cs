using System.Text;
using System.Text.Json;

namespace Dinotrack.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> PostAsync<T, TResponse>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, messageContent);
            if (response.IsSuccessStatusCode)
            {
                var responseU = await UnserializeAnswer<TResponse>(response);
                return new HttpResponseWrapper<TResponse>(responseU, false, response);
            }

            return new HttpResponseWrapper<TResponse>(default, !response.IsSuccessStatusCode, response);
        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> PutAsync<T, TResponse>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, messageContent);
            if (response.IsSuccessStatusCode)
            {
                var responseU = await UnserializeAnswer<TResponse>(response);
                return new HttpResponseWrapper<TResponse>(responseU, false, response);
            }

            return new HttpResponseWrapper<TResponse>(default, !response.IsSuccessStatusCode, response);
        }
    }
}