using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Cloud.InstrumentationFramework;
using Microsoft.Extensions.Logging;
using SampleIfxApp.Ifx;

namespace SampleIfxApp.Controllers
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
        public IEnumerable<WeatherForecast> Get()
        {
            System.Console.WriteLine("Came to Weather Forecast Api");
            // MDM based APIs do not need Ifx initialization.
            IfxUtil.MdmSample();

            // Health based APIs do not need Ifx initialization.
            IfxUtil.HealthSample();

            // Prior to invoking any Ifx APIs we first initialize Ifx.
            // For init with session name, Read this SO answer: http://stackoverflow.microsoft.com/a/6855/748
            IfxInitializer.Initialize("saianu", "unifiedtestmetrics", "SAI-WORK-PC");


            IfxUtil.IfxObjectSamples();
            IfxUtil.TracingSample();
            IfxUtil.OperationsSample();
            IfxUtil.ExtendedOperationsSample();
            IfxUtil.PartADerivedEventSample();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
