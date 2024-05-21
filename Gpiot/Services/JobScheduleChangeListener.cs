using Gpiot.Constants;
using Gpiot.Interfaces;
using Gpiot.Models;
using nanoFramework.Hosting;
using System.Threading;

namespace Gpiot.Services
{
    public class JobScheduleChangeListener : BackgroundService
    {
        private readonly IJobScheduleManager _jobScheduleManager;

        public JobScheduleChangeListener(IJobScheduleManager jobScheduleManager)
        {
            _jobScheduleManager = jobScheduleManager;                            
        }
        protected override void ExecuteAsync()
        {
            while (!CancellationRequested)
            {
                var scheduleChanged = StateManager.GetInstance().GetState(AppState.SCHEDULE_CHANGED);
                if (scheduleChanged.Equals("true"))
                {
                    var schedules = GpioPinSchedule.GetSchedulesFromState();
                    _jobScheduleManager.UpdateJobConfigurations(schedules);
                    StateManager.GetInstance().SetState(AppState.SCHEDULE_CHANGED, "false");
                }

                Thread.Sleep(1000 * 60);
            }
        }
    }
}
