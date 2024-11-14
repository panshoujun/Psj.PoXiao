using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Psj.PoXiao.WebApi.Controllers
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

        [Route("GetWeatherForecast")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var connString = "Host=localhost;Database=postgres;Username=postgres;Password=123456";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT version()", conn))
                {
                    var version = cmd.ExecuteScalar();
                    Console.WriteLine($"PostgreSQL version: {version}");
                }
            }


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("GetPgSqlVersion")]
        [HttpGet]
        public string GetPgSqlVersion()
        {
            var result = "";
            var connString = "Host=localhost;Database=postgres;Username=postgres;Password=123456";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT version()", conn))
                {
                    var version = cmd.ExecuteScalar();
                    result = $"PostgreSQL version: {version}";
                    Console.WriteLine($"PostgreSQL version: {version}");
                }
            }
            return result;
        }
    }
}
