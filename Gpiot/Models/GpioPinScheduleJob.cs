using System;

namespace Gpiot.Models
{
    public class GpioPinScheduleJob : GpioPinSchedule
    {
        public GpioPinScheduleJob(GpioPinSchedule gpioPinSchedule)
        {
            Pin = gpioPinSchedule.Pin;
            Name = gpioPinSchedule.Name;
            Start = gpioPinSchedule.Start;
            Interval = gpioPinSchedule.Interval;
        }

        public Action OnStart { get; set; }
        public Action OnStop { get; set; }
    }
}
