using Gpiot.Helpers;
using Gpiot.Models;
using nanoFramework.Json;
using nanoFramework.WebServer;
using System.Text;

namespace Gpiot.Controllers
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
                var scheduleManager = new ScheduleStateManager();
                var scheduleAdded = scheduleManager.AddSchedule((GpioPinSchedule) gpioPinSchedule);
                if (scheduleAdded)
                {
                    WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.Created);
                }
                else
                {
                    WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.InternalServerError);            
            }            
        }

        [Route("schedule")]
        [Method("GET")]
        public void GetSchedule(WebServerEventArgs e)
        {
            try
            {
                var schedules = GpioPinScheduleHelper.GetSchedulesFromState();
                string json = "string.Empty";
                json = JsonSerializer.SerializeObject(schedules);

                if (!string.IsNullOrEmpty(json))
                {
                    WebServer.OutPutStream(e.Context.Response, json);
                }
                else
                {
                    WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.NotFound);
                }
            }
            catch
            {
                WebServer.OutputHttpCode(e.Context.Response, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
