using DevWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.ViewModels
{
    public class RootObject_VM
    {
        private RootObject rootObject;

        public RootObject_VM( RootObject _rootObject)
        {
            this.rootObject = _rootObject;
        }
        public List<Weather> Weathers
        {
            get { return this.rootObject.weather; }
            set { rootObject.weather = value; }
        }

        public Coord Coord
        {
            get { return this.rootObject.coord;  }
            set { }
        }
    }
}
