using nanoFramework.Hosting;
using nanoFramework.WebServer;
using System;

namespace IrrigationControl.Services
{
    internal class WebService : IHostedService
    {
        private WebServer _server;

        public void Start()
        {
            _server = new WebServer(3000, HttpProtocol.Http);            
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}
