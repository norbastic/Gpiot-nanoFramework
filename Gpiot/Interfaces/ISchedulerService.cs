using Gpiot.Models;
using System.Collections;

namespace Gpiot.Interfaces
{
    public interface ISchedulerService
    {
        void UpdateJobConfigurations(GpioPinSchedule[] newConfigurations);
    }
}
