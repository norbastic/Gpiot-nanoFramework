using IrrigationControl.Interfaces;
using nanoFramework.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace IrrigationControl.Services
{
    public class WIFIService : SchedulerService
    {
        // Let's reconnect to WIFI every 8 hours 
        public WIFIService(TimeSpan interval, IStateManager _stateManager) : base(TimeSpan.FromHours(8))
        {
        }

        protected override void ExecuteAsync()
        {
            try
            {
                File.ReadAllText(@"I:\\WIFI.json");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
                       
        }
    }
}
