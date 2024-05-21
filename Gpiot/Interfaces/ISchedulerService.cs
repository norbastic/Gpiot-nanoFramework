using System.Collections;

namespace Gpiot.Interfaces
{
    public interface ISchedulerService
    {
        void UpdateJobConfigurations(ArrayList newConfigurations);
    }
}
