using Gpiot.Constants;
using Gpiot.Services;
using System;
using System.Collections;
using nanoFramework.Json;

namespace Gpiot.Models
{
    public class GpioPinSchedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pin { get; set; }
        public string Start { get; set; }
        public int Interval { get; set; }
    }
}
