using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaAPI.model.objects
{
    public class List
    {
        public int dt { get; set; }
        public MainForecast main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public SysForecast sys { get; set; }
        public string dt_txt { get; set; }
    }
}
