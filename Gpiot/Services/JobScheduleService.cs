using Gpiot.Interfaces;
using nanoFramework.Hosting;
using System;

namespace Gpiot.Services
{
    public class JobScheduleService : IHostedService
    {
        private readonly IJobScheduleManager _jobScheduleManager;

        public JobScheduleService(IJobScheduleManager jobScheduleManager)
        {
            _jobScheduleManager = jobScheduleManager;                
        }
        public void Start()
        {
            _jobScheduleManager.Start();
        }

        public void Stop()
        {
            _jobScheduleManager.Stop();
        }
    }
}
