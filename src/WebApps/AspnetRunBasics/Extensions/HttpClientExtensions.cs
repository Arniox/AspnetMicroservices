using System;
using System.Net.Http;
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
    }
}
