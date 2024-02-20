using Lab5.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab5
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }


        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error occurred while processing the request.");
            }
        }

        public async Task<string> PostAsync(string url, object data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error occurred while processing the request.");
            }
        }

        //public async Task<string> PostAsync(string url, IDictionary<string, string> data)
        //{
        //    try
        //    {
        //        //var jsonData = JsonConvert.SerializeObject(query);
        //        //var content = new StringContent(jsonData);
        //        //var content = new FormUrlEncodedContent(data);
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, (HttpContent?)data);
        //        response.EnsureSuccessStatusCode();
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //    catch (HttpRequestException)
        //    {
        //        throw new Exception("Error occurred while processing the request.");
        //    }
        //}


        //public async Task<string> PostAsync(string url, HttpContent q)
        //{
        //    try
        //    {
        //        //var jsonData = JsonConvert.SerializeObject(q);
        //        //var content = new StringContent(q);
        //        //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, q);
        //        response.EnsureSuccessStatusCode();
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //    catch (HttpRequestException)
        //    {
        //        throw new Exception("Error occurred while processing the request.");
        //    }
        //}

        //public async Task<string> PostAsync(string url, IDictionary<string, string> data)
        //{
        //    try
        //    {
        //        var content = new FormUrlEncodedContent(data);
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //    catch (HttpRequestException)
        //    {
        //        throw new Exception("Error occurred while processing the request.");
        //    }
        //}
    }
}