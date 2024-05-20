using IrrigationControl.Models;
using nanoFramework.Json;
using nanoFramework.WebServer;
using System.Text;

namespace IrrigationControl.Controllers
{
    public class ControllerScheduler
    {
        [Route("schedule")]
        [Method("POST")]
        public void AddSchedule(WebServerEventArgs e)
        {
            byte[] buff = new byte[e.Context.Request.ContentLength64];
            e.Context.Request.InputStream.Read(buff, 0, buff.Length);
            var rawData = new string(Encoding.UTF8.GetChars(buff));

            if (string.IsNullOrEmpty(rawData))
            {
                WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var gpioPinSchedule = JsonConvert.DeserializeObject(rawData, typeof(GpioPinSchedule));
                WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.Created);
            }
            catch
            {
                WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.InternalServerError);            
            }            
        }
    }
}
