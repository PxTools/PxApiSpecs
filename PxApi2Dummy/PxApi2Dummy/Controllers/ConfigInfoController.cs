using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Data;

namespace PxApi2Dummy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigInfoController : ControllerBase
    {
        

        private readonly ILogger<ConfigInfoController> _logger;

        public ConfigInfoController(ILogger<ConfigInfoController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetConfigInfo")]
        public ConfigInfo Get()
        {
            return SampleData.SampleConfigInfo.Get();
        }


        /*
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
         */

    }
}