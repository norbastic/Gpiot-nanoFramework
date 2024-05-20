using System.Collections;

namespace IrrigationControl.Interfaces
{
    public interface ISchedulerService
    {
        void UpdateJobConfigurations(ArrayList newConfigurations);
    }
}
