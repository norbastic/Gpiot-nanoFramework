using nanoFramework.WebServer;

namespace IrrigationControl.Controllers
{
    public class ControllerIndex
    {

        private const string IndexHTML =
        @"<html>
            <head>
                <title>
                    Irrigation Control
                </title>
            </head>
            <body>
                <h1>Hello from Irrigation Control</h1>
            </body>
        </html>";

        [Route("/")]
        [Method("GET")]
        public void RouteGetIndex(WebServerEventArgs e)
        {
            e.Context.Response.ContentType = "text/html";
            WebServer.OutPutStream(e.Context.Response, IndexHTML);
        }
    }
}
