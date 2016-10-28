using DevWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.ViewModels
{
    public class Clouds_VM
    {
        private Clouds clouds;

        public Clouds_VM(Clouds _clouds)
        {
            this.clouds = _clouds;
        }
        public int all
        {
        get
            {
                if (this.clouds == null)
                    return 0;
                return this.clouds.all;
            }
        set
            {
                if (this.clouds != null)
                    this.clouds.all = value;
            }
        }
    }
}
