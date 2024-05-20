using IrrigationControl.Constants;
using IrrigationControl.Services;
using System;
using System.Collections;

namespace IrrigationControl.Models
{
    public class GpioPinSchedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PinNumber { get; set; }
        public string Start { get; set; }
        public int Interval { get; set; }
        public Action OnStart { get; set; }
        public Action OnStop { get; set; }

        public static ArrayList GetSchedulesFromState()
        {
            ArrayList schedules = new ArrayList();
            var schedule1 = StateManager.GetInstance().GetState(AppState.SCHEDULE1_NAME);
            if (!string.IsNullOrEmpty(schedule1))
            {
                schedules.Add(
                    new GpioPinSchedule
                    {
                        Id = 1,
                        Name = StateManager.GetInstance().GetState(AppState.SCHEDULE1_NAME),
                        Start = StateManager.GetInstance().GetState(AppState.SCHEDULE1_START),
                        PinNumber = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE1_PIN)),
                        Interval = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE1_INTERVAL))
                    }
                );
            }
            var schedule2 = StateManager.GetInstance().GetState(AppState.SCHEDULE2_NAME);
            if (!string.IsNullOrEmpty(schedule2))
            {
                schedules.Add(
                    new GpioPinSchedule
                    {
                        Id = 2,
                        Name = StateManager.GetInstance().GetState(AppState.SCHEDULE2_NAME),
                        Start = StateManager.GetInstance().GetState(AppState.SCHEDULE2_START),
                        PinNumber = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE2_PIN)),
                        Interval = Convert.ToInt32(StateManager.GetInstance().GetState(AppState.SCHEDULE2_INTERVAL))
                    }
                );
            }

            return schedules;            
        }
    }
}
