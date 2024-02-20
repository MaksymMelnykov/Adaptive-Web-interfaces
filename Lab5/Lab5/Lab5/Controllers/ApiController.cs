using Lab5.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Lab5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly string apiKey = "Xfu8OBR9pDBKbe1o9qNNi2mYDI7RSRbB";

        public ApiController(HttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetResultAsync()
        {
            var response = new ApiResponse<string>();

            try
            {
                string responseData = await _httpClientWrapper
                    .GetAsync($"https://api.giphy.com/v1/gifs/random?api_key={apiKey}");

                response.Data = new List<string> { responseData };
                response.Message = "Request successful!";
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> PostResultAsync([FromBody] MiataruGetLocationRequest request)
        {
            var response = new ApiResponse<string>();

            try
            {
                var jsonData = JsonConvert.SerializeObject(request);
                string responseData = await _httpClientWrapper.PostAsync("http://service.miataru.com/v1/GetLocation", request);
                response.Data = new List<string> { responseData };
                response.Message = "Request successful!";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return StatusCode((int)response.StatusCode, response);
        }

        //[HttpPost]
        //public async Task<IActionResult> PostResultAsync([FromBody] HttpContent q)
        //{
        //    var response = new ApiResponse<string>();

        //    try
        //    {
        //        //    var queryParams = new Dictionary<string, string>
        //        //    {
        //        //        ["api_key"] = apiKey,
        //        //        ["q"] = query,
        //        //        ["limit"] = "25",
        //        //        ["offset"] = "0",
        //        //        ["rating"] = "g",
        //        //        ["lang"] = "en",
        //        //        ["bundle"] = "messaging_non_clips"
        //        //    };

        //        //    var httpContent = new FormUrlEncodedContent(queryParams);

        //        //var content = new StringContent(JsonConvert.SerializeObject(q), Encoding.UTF8, "application/json");
        //        var jsonData = JsonConvert.SerializeObject(q);
        //        string responseData = await _httpClientWrapper.PostAsync($"https://jsonplaceholder.typicode.com/posts", q);
        //        response.Data = new List<string> { responseData };
        //        response.Message = "Request successful!";
        //        response.StatusCode = HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        response.StatusCode = HttpStatusCode.InternalServerError;
        //    }

        //    return StatusCode((int)response.StatusCode, response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> PostResultAsync(string q)
        //{
        //    var response = new ApiResponse<string>();

        //    try
        //    {
        //        string responseData = await _httpClientWrapper
        //            .PostAsync($"https://api.giphy.com/v1/gifs/search?api_key={apiKey}", q);
        //        response.Data = new List<string> { responseData };
        //        response.Message = "Request successful!";
        //        response.StatusCode = System.Net.HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //    }

        //    return StatusCode((int)response.StatusCode, response);
        //}
    }
}