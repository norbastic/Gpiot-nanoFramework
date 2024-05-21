using Gpiot.Constants;
using Gpiot.Models;
using Gpiot.Services;
using System;

namespace Gpiot.Helpers
{
    public class GpioPinScheduleHelper
    {
        public static GpioPinSchedule[] GetSchedulesFromState()
        {
            GpioPinSchedule[] schedules = new GpioPinSchedule[2];
            var schedule1 = StateManager.GetInstance().GetState(AppState.SCHEDULE1_NAME);
            if (!string.IsNullOrEmpty(schedule1))
            {
                schedules[0] =
                    new GpioPinSchedule
                    {
                        Id = 1,
                        Name = StateManager.GetInstance().GetState(AppState.SCHEDULE1_NAME),
                        Start = StateManager.GetInstance().GetState(AppState.SCHEDULE1_START),
                        Pin = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE1_PIN)),
                        Interval = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE1_INTERVAL))
                    };
            }
            var schedule2 = StateManager.GetInstance().GetState(AppState.SCHEDULE2_NAME);
            if (!string.IsNullOrEmpty(schedule2))
            {
                schedules[1] =
                    new GpioPinSchedule
                    {
                        Id = 2,
                        Name = StateManager.GetInstance().GetState(AppState.SCHEDULE2_NAME),
                        Start = StateManager.GetInstance().GetState(AppState.SCHEDULE2_START),
                        Pin = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE2_PIN)),
                        Interval = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE2_INTERVAL))
                    };
            }

            return schedules;
        }
    }
}
