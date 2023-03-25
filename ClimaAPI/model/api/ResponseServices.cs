using ClimaAPI.model.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaAPI.model.api
{
    public class ResponseServices
    {
        public bool Error { get; set; }
        public String Message { get; set; }
        public CurrentWeather WeatherReport { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
    }
}
