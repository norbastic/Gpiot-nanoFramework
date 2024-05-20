using IrrigationControl.Models;

namespace IrrigationControl.Interfaces
{
    public interface IScheduleStateManager
    {
        bool AddSchedule(GpioPinSchedule schedule);
    }
}