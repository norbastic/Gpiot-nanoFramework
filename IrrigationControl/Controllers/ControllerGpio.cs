using nanoFramework.WebServer;
using System.Net;
using System;
using IrrigationControl.Helper;
using IrrigationControl.Models;

namespace IrrigationControl.Controllers
{
    public class ControllerGpio
    {
        
        private int GetPinFromUrl(WebServerEventArgs e)
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

        private PinInfo GetPinAndValueFromUrl(WebServerEventArgs e)
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

                return new PinInfo {
                    PinNumber = Convert.ToInt32(pin),
                    Value = Convert.ToInt32(value)
                };
            }
            catch
            {
                return null;
            }
        }

        [Route("activatepin")]
        [Method("GET")]
        public void SetPinActive(WebServerEventArgs e)
        {            
            var pinNumber = GetPinFromUrl(e);
            if (pinNumber == -1) {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }

            var pinActivated = GpioHelper.ActivatePinOutput(pinNumber);

            if (pinActivated) {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.OK);
            }
            else {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }
        }

        [Route("deactivatepin")]
        [Method("GET")]
        public void SetPinOff(WebServerEventArgs e)
        {
            var pinNumber = GetPinFromUrl(e);
            if (pinNumber == -1)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }

            var pinDeactivated = GpioHelper.DeactivatePinOutput(pinNumber);

            if (pinDeactivated)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.OK);
            }
            else
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }
        }

        [Route("setpinvalue")]
        [Method("GET")]
        public void SetPinValue(WebServerEventArgs e)
        {
            var pinPayload = GetPinAndValueFromUrl(e);
            if (pinPayload == null)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
                return;
            }

            if (!GpioHelper.IsPinActive(pinPayload.PinNumber))
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
                return;
            }

            var valueWritten = GpioHelper.WriteOutputToPin(pinPayload); 

            if (valueWritten)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.OK);
            }
            else
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }
        }
    }
}
