using Lab6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();
        private static int _nextId = 1;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecasts;
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Create([FromBody] WeatherForecast weatherForecast)
        {
            try
            {
                weatherForecast.Id = _nextId++;
                weatherForecast.Date = DateOnly.FromDateTime(DateTime.Now);
                weatherForecast.TemperatureC = Random.Shared.Next(-20, 55);
                weatherForecast.Summary = Summaries[Random.Shared.Next(Summaries.Length)];

                _weatherForecasts.Add(weatherForecast);

                return Ok("WeatherForecast created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating WeatherForecast: {ex.Message}");
            }
        }

        [HttpPut("{id}", Name = "PutWeatherForecast")]
        public IActionResult Update(int id, [FromBody] WeatherForecast weatherForecast)
        {
            try
            {
                var existingForecast = _weatherForecasts.FirstOrDefault(f => f.Id == id);
                if (existingForecast == null)
                {
                    return NotFound($"WeatherForecast with ID {id} not found");
                }

                existingForecast.TemperatureC = weatherForecast.TemperatureC;
                existingForecast.Summary = weatherForecast.Summary;

                return Ok($"WeatherForecast with ID {id} updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating WeatherForecast: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingForecast = _weatherForecasts.FirstOrDefault(f => f.Id == id);
                if (existingForecast == null)
                {
                    return NotFound($"WeatherForecast with ID {id} not found");
                }

                _weatherForecasts.Remove(existingForecast);

                return Ok($"WeatherForecast with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting WeatherForecast: {ex.Message}");
            }
        }
    }
}