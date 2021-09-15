using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minave.App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Minave.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://minavefunctions:80/api/WeatherForeCastCalc");

            if (response.IsSuccessStatusCode)
            {

                string responseJson = await response.Content.ReadAsStringAsync();
                var weatherForecastList = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(responseJson);
                return weatherForecastList;
            }
            return Enumerable.Empty<WeatherForecast>();
        }
    }
}
