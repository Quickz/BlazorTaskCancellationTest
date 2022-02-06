using BlazorTaskCancellationTest.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTaskCancellationTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<WeatherForecast>> Get(CancellationToken token)
        {
            var data = new List<WeatherForecast>();

            for (int i = 0; i < 5; i++)
            {
                // Artifical delay
                await Task.Delay(500);

                token.ThrowIfCancellationRequested();

                var instance = new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i + 1),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                };

                data.Add(instance);
            }

            return data;
        }
    }
}