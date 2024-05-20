using IrrigationControl.Constants;
using IrrigationControl.Interfaces;
using IrrigationControl.Services;
using nanoFramework.Hosting;

namespace IrrigationControl
{
    public class Program
    {
        public static void Main()
        {
            //StateManager.GetInstance().SetState(AppState.WIFI_SSID, "TO_BE_FILLED");
            // StateManager.GetInstance().SetState(AppState.WIFI_PASSWORD, "TO_BE_FILLED");
            IHost host = CreateHostBuilder().Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService(typeof(WIFIService));
                services.AddHostedService(typeof(WebService));                                
                services.AddHostedService(typeof(JobScheduleService));
                services.AddHostedService(typeof(ScheduleChangeListener));
            });
    }
}
