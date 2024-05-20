using IrrigationControl.Constants;
using IrrigationControl.Interfaces;
using IrrigationControl.Services;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.Hosting;
using System.IO;
using System.Resources;

namespace IrrigationControl
{
    public class Program
    {
        public static void Main()
        {
            //StateManager.GetInstance().SetState(AppState.WIFI_SSID, "TO_BE_FILLED");
            // StateManager.GetInstance().SetState(AppState.WIFI_PASSWORD, "TO_BE_FILLED");

            var resourceManager = new ResourceManager("IrrigationControl.Resources", typeof(Program).Assembly);

            var dirs = Directory.GetDirectories("I:\\");
            var files = Directory.GetFiles("I:\\");

            IHost host = CreateHostBuilder().Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.TryAdd(new ServiceDescriptor(typeof(IStateManager), StateManager.GetInstance()));
                services.AddHostedService(typeof(WebService));
                services.AddHostedService(typeof(WIFIService));                
                services.AddHostedService(typeof(IrrigationScheduleService));
            });
    }
}
