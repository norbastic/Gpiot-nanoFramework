using Gpiot.Models;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System;
using Gpiot.Helpers;
using Gpiot.Interfaces;

namespace Gpiot.Services
{
    public class JobScheduleManager : IDisposable, IJobScheduleManager
    {
        private Timer _timer;
        private ArrayList _currentJobs = new();

        public JobScheduleManager()
        {
            _currentJobs = GpioPinSchedule.GetSchedulesFromState();
            UpdateOnStartOnStop();
        }

        public void Start()
        {
            ScheduleJobs();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Change(Timeout.Infinite, 0);
            }
            return;
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

        public void UpdateJobConfigurations(ArrayList newConfigurations)
        {
            _currentJobs = newConfigurations;
            UpdateOnStartOnStop();
            ScheduleJobs();
        }

        private bool TimeMatches(DateTime now, string scheduleTime)
        {
            var parts = scheduleTime.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
            {
                var scheduledTime = new TimeSpan(hours, minutes, 0);
                return now.TimeOfDay >= scheduledTime && now.TimeOfDay < scheduledTime.Add(TimeSpan.FromMinutes(1));
            }
            return false;
        }

        private void ExecuteJobs(object state)
        {
            var now = DateTime.UtcNow;
            foreach (GpioPinSchedule job in _currentJobs)
            {
                if (TimeMatches(now, job.Start))
                {
                    Debug.WriteLine($"Executing job: {job.Name}");
                    job.OnStart();
                    Thread.Sleep(job.Interval * 60 * 1000);
                    job.OnStop();
                }
            }
        }

        private void ScheduleJobs()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }

            _timer = new Timer(ExecuteJobs, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private void UpdateOnStartOnStop()
        {
            foreach (GpioPinSchedule job in _currentJobs)
            {
                job.OnStart = () =>
                {
                    GpioHelper.ActivatePinOutput(job.PinNumber);
                    GpioHelper.WriteOutputToPin(new PinInfo
                    {
                        PinNumber = job.PinNumber,
                        Value = 1
                    });
                };
                job.OnStop = () =>
                {
                    GpioHelper.WriteOutputToPin(new PinInfo
                    {
                        PinNumber = job.PinNumber,
                        Value = 0
                    });
                    GpioHelper.DeactivatePinOutput(job.PinNumber);
                };
            }
        }
    }
}
