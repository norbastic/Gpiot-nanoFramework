using Gpiot.Models;

namespace Gpiot.Interfaces
{
    public interface IScheduleStateManager
    {
        bool AddSchedule(GpioPinSchedule schedule);
    }
}