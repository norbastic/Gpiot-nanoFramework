using nanoFramework.WebServer;
using System.Net;
using Gpiot.Helpers;
using System.Device.Gpio;
using nanoFramework.Json;
using Gpiot.Models;

namespace Gpiot.Controllers
{
    public class ControllerGpio
    {      
        [Route("activatepin")]
        [Method("GET")]
        public void SetPinActive(WebServerEventArgs e)
        {            
            var pinNumber = UrlParser.GetPinFromUrl(e);
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
            var pinNumber = UrlParser.GetPinFromUrl(e);
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
            var pinPayload = UrlParser.GetPinAndValueFromUrl(e);
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

        [Route("pinstatus")]
        [Method("GET")]
        public void GetPinStatus(WebServerEventArgs e)
        {
            var pinNumber = UrlParser.GetPinFromUrl(e);

            var open = GpioHelper.IsPinActive(pinNumber);
            var pinValue = GpioHelper.GetPinValue(pinNumber);

            try
            {
                var json = JsonSerializer.SerializeObject(
                    new PinStatus
                    {
                        PinNumber = pinNumber,
                        Open = open.ToString(),
                        Value = pinValue == PinValue.Low ? 0 : 1
                    }
                    );
                WebServer.OutPutStream(e.Context.Response, json);
            }
            catch
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }
        }
    }
}
