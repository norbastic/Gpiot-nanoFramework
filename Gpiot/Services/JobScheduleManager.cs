using Gpiot.Models;
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
        private GpioPinScheduleJob[] _currentJobs = new GpioPinScheduleJob[2];

        public JobScheduleManager()
        {
            var savedSchedules = GpioPinScheduleHelper.GetSchedulesFromState();
            UpdateCurrentJobs(savedSchedules);
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

        public void UpdateJobConfigurations(GpioPinSchedule[] newConfigurations)
        {
            UpdateCurrentJobs(newConfigurations);
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
            foreach (GpioPinScheduleJob job in _currentJobs)
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
            foreach (GpioPinScheduleJob job in _currentJobs)
            {
                job.OnStart = () =>
                {
                    GpioHelper.ActivatePinOutput(job.Pin);
                    GpioHelper.WriteOutputToPin(new PinInfo
                    {
                        PinNumber = job.Pin,
                        Value = 1
                    });
                };
                job.OnStop = () =>
                {
                    GpioHelper.WriteOutputToPin(new PinInfo
                    {
                        PinNumber = job.Pin,
                        Value = 0
                    });
                    GpioHelper.DeactivatePinOutput(job.Pin);
                };
            }
        }

        private void UpdateCurrentJobs(GpioPinSchedule[] gpioPinSchedules)
        {
            for (int i = 0; i < gpioPinSchedules.Length; i++)
            {
                _currentJobs[i] = new GpioPinScheduleJob(gpioPinSchedules[i]);
            }
        }
    }
}
