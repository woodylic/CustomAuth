using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomAuth.Client
{
    public class CustomAuthClient : ICustomAuthClient
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthClientOptions _options;

        public CustomAuthClient(HttpClient httpClient, IOptions<CustomAuthClientOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }
        
        public async Task<UserProfile> GetUserProfileAsync(string token)
        {
            var baseUri = new Uri(_options.Host);
            var requestUri = new Uri(baseUri, $"/api/token/{token}");
            var response = await _httpClient.GetAsync(requestUri);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Token is invalid or expired. {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var userProfile = JsonConvert.DeserializeObject<UserProfile>(json);

            return userProfile;
        }
    }
}