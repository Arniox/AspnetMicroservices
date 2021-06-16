using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspnetRunBasics.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Read content from response to Json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns>A <typeparamref name="T"/> representation of the JSON Value</returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage response)
        {
            //Check error
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            //Get string
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        /// <summary>
        /// Post data as <typeparamref name="T"/> object to HttpClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns>HttpClient Post HttpResponseMessage</returns>
        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            //Object data into string
            var dataString = JsonSerializer.Serialize(data);

            //Set up request call with body
            var content = new StringContent(dataString);
            //Set up request call with header
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Return HttpClient Call
            return httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Put data as <typeparamref name="T"/> object to HttpClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns>httpClient Post HttpResponseMessage</returns>
        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            //Object data into string
            var dataString = JsonSerializer.Serialize(data);

            //Set up request call with body
            var content = new StringContent(dataString);
            //Set up request call with header
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Return HttpClient call
            return httpClient.PutAsync(url, content);
        }
    }
}
