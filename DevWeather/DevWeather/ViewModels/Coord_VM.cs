using DevWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.ViewModels
{
    public class Coord_VM
    {
        private Coord coord;

        public Coord_VM (Coord _coord)
        {
            this.coord = _coord;
        }
        public double Lon
        {
            get
            {
                if (this.coord == null)
                {
                    return 0;
                }
                return this.coord.lon;
            }
            set
            {
                if (this.coord != null)
                {
                    this.coord.lon = value;

                }
            }
        }
        public double Lat
        {
            get
            {
                if (this.coord == null)
                {
                    return 0;
                }
                return this.coord.lat;
            }
            set
            {
                if (this.coord != null)
                {
                    this.coord.lat = value;

                }
            }
        }
    }
}
