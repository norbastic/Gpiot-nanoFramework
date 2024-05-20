using IrrigationControl.Models;
using nanoFramework.WebServer;
using System.Net;
using System;

namespace IrrigationControl.Helpers
{
    public static class UrlParser
    {
        internal static int GetPinFromUrl(WebServerEventArgs e)
        {
            var rawUrl = e.Context.Request.RawUrl.TrimStart('/');
            var args = rawUrl.Split('?');
            if (args.Length < 2)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }

            try
            {
                var pin = args[1].Split('=')[1];
                var pinNumber = Convert.ToInt32(pin);
                return pinNumber;
            }
            catch
            {
                return -1;
            }
        }

        internal static PinInfo GetPinAndValueFromUrl(WebServerEventArgs e)
        {
            var rawUrl = e.Context.Request.RawUrl.TrimStart('/');
            var args = rawUrl.Split('?');
            if (args.Length < 2)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }

            try
            {
                var parameters = args[1].Split('&');
                var pin = parameters[0].Split('=')[1];
                var value = parameters[1].Split('=')[1];

                return new PinInfo
                {
                    PinNumber = Convert.ToInt32(pin),
                    Value = Convert.ToInt32(value)
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
