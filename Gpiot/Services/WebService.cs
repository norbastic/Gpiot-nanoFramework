using Gpiot.Controllers;
using nanoFramework.Hosting;
using nanoFramework.WebServer;
using System;

namespace Gpiot.Services
{
    internal class WebService : IHostedService
    {
        private WebServer _server;

        public void Start()
        {
            _server = new WebServer(
                3000,
                HttpProtocol.Http,
                new Type[] {
                    typeof(ControllerIndex),
                    typeof(ControllerGpio),
                    typeof(ControllerScheduler),
                });
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
            _server.Dispose();
        }
    }
}
