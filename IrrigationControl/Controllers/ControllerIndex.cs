using nanoFramework.WebServer;

namespace IrrigationControl.Controllers
{
    internal class ControllerIndex
    {
        [Route("index")]
        [Method("GET")]
        public void Index(WebServerEventArgs e)
        {
            e.Context.Response.ContentType = "text/plain";
            WebServer.OutPutStream(e.Context.Response, "Hello World!");
        }
    }
}
