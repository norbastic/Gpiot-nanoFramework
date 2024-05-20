using IrrigationControl.Constants;
using nanoFramework.Hosting;
using System.Threading;

namespace IrrigationControl.Services
{
    public class ScheduleChangeListener : BackgroundService
    {
        protected override void ExecuteAsync()
        {
            while (!CancellationRequested)
            {
                var scheduleChanged = StateManager.GetInstance().GetState(AppState.SCHEDULE_CHANGED);
                if (scheduleChanged.Equals("true"))
                { 
                    // Act                
                }

                Thread.Sleep(1000 * 60);
            }
        }
    }
}
