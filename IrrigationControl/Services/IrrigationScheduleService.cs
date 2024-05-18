using nanoFramework.Hosting;
using System;
using System.Diagnostics;

namespace IrrigationControl.Services
{
    internal class IrrigationScheduleService : SchedulerService
    {
        public IrrigationScheduleService(TimeSpan interval) : base(interval)
        {
        }

        protected override void ExecuteAsync()
        {
            Debug.WriteLine("Schedule triggered.");
        }
    }
}
