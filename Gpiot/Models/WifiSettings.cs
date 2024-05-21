namespace Gpiot.Models
{
    internal class WifiSettings
    {
        public string SSID { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string Netmask { get; set; }
        public string GatewayAddress { get; set; }
        public string DNSAddress { get; set;}
    }
}
