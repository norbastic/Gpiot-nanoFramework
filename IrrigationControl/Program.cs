using IrrigationControl.Interfaces;
using IrrigationControl.Services;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.Hosting;
using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.Threading;

namespace IrrigationControl
{
    public class Program
    {
        public static void Main()
        {
            var ver = StateManager.GetInstance().GetState("ApplicationVersion");
            Debug.WriteLine($"Hello from nanoFramework, ver: {ver}");

            IHost host = CreateHostBuilder().Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService(typeof(WIFIService));
                services.AddSingleton(typeof(IStateManager), typeof(StateManager));
                services.AddHostedService(typeof(IrrigationScheduleService));
            });
    }
}
