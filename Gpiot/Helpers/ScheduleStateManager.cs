using Gpiot.Constants;
using Gpiot.Interfaces;
using Gpiot.Models;
using Gpiot.Services;
using System;

namespace Gpiot.Helpers
{
    public class ScheduleStateManager : IScheduleStateManager
    {
        private void AddScheduleToState(int scheduleId, GpioPinSchedule schedule)
        {
            StateManager.GetInstance().SetState(AppState.SCHEDULE_CHANGED, "true");
            StateManager.GetInstance().SetState(scheduleId == 1 ? AppState.SCHEDULE1_NAME : AppState.SCHEDULE2_NAME, schedule.Name);
            StateManager.GetInstance().SetState(scheduleId == 1 ? AppState.SCHEDULE1_PIN : AppState.SCHEDULE2_PIN, schedule.Pin.ToString());
            StateManager.GetInstance().SetState(scheduleId == 1 ? AppState.SCHEDULE1_START : AppState.SCHEDULE2_START, schedule.Start);
            StateManager.GetInstance().SetState(scheduleId == 1 ? AppState.SCHEDULE1_INTERVAL : AppState.SCHEDULE2_INTERVAL, schedule.Interval.ToString());
        }

        public bool AddSchedule(GpioPinSchedule schedule)
        {
            var savedSchedules = GpioPinScheduleHelper.GetSchedulesFromState();
            // No schedules have been added yet
            if (savedSchedules.Length == 0)
            {
                AddScheduleToState(1, schedule);
                return true;
            }

            bool foundExisting = false;

            foreach (GpioPinSchedule savedSchedule in savedSchedules)
            {
                // We override the existing one
                if (savedSchedule.Name.ToLower().Contains(schedule.Name.ToLower()))
                {
                    foundExisting = true;
                    AddScheduleToState(savedSchedule.Id, schedule);
                    return true;
                }
            }

            if (foundExisting == false && savedSchedules.Length == 1)
            {
                // We assume that only one schedule is stored
                AddScheduleToState(2, schedule);
                return true;
            }

            if (foundExisting == false && savedSchedules.Length == 2)
            {
                // Only two schedules supported...
                return false;
            }

            return false;
        }
    }
}
