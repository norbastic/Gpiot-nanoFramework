using Gpiot.Models;
using System.Collections;

namespace Gpiot.Interfaces
{
    public interface IJobScheduleManager
    {
        void Start();
        void Stop();
        void UpdateJobConfigurations(GpioPinSchedule[] newConfigurations);
    }
}