using Gpiot.Constants;
using Gpiot.Models;
using nanoFramework.Hosting;
using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.Threading;

namespace Gpiot.Services
{
    public class WIFIService : SchedulerService
    {
        // Let's reconnect to WIFI every 8 hours 
        public WIFIService(TimeSpan interval) : base(TimeSpan.FromHours(8))
        {
        }

        protected override void ExecuteAsync()
        {
            var wifiSettings = GetWifiSettings();
            ValidateWifiSettings(wifiSettings);

            if (!string.IsNullOrEmpty(wifiSettings.IPAddress) ||
                !string.IsNullOrEmpty(wifiSettings.Netmask) ||
                !string.IsNullOrEmpty(wifiSettings.GatewayAddress)
                )
            {
                ConnectUsingStaticIp(wifiSettings);
            }
            else
            {
                ConnectUsingDHCP(wifiSettings);
            }                       
        }

        private WifiSettings GetWifiSettings() =>
            new()
            {
                SSID = StateManager.GetInstance().GetState(AppState.WIFI_SSID),
                Password = StateManager.GetInstance().GetState(AppState.WIFI_PASSWORD),
                IPAddress = StateManager.GetInstance().GetState(AppState.WIFI_STATIC_IP),
                Netmask = StateManager.GetInstance().GetState(AppState.WIFI_STATIC_IP_NETMASK),
                GatewayAddress = StateManager.GetInstance().GetState(AppState.WIFI_STATIC_IP_GW),
                DNSAddress = StateManager.GetInstance().GetState(AppState.WIFI_STATIC_IP_DNS),
            };
        
        private void ValidateWifiSettings(WifiSettings wifiSettings) {
            if (string.IsNullOrEmpty(wifiSettings.SSID) || string.IsNullOrEmpty(wifiSettings.Password))
            {
                throw new ArgumentException("You need to set initial WIFI SSID and Password!");
            }
        }

        private void ConnectUsingDHCP(WifiSettings wifiSettings)
        {
            CancellationTokenSource cs = new(60000);
            var success = WifiNetworkHelper.ScanAndConnectDhcp(wifiSettings.SSID, wifiSettings.Password, requiresDateTime: true, token: cs.Token);
            if (!success)
            {
                Debug.WriteLine($"Can't connect to the network, error: {WifiNetworkHelper.Status}");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"ex: {WifiNetworkHelper.HelperException}");
                }
            }
        }

        private void ConnectUsingStaticIp(WifiSettings wifiSettings)
        {
            CancellationTokenSource cs = new(60000);
            var success = WifiNetworkHelper.ConnectFixAddress(
                wifiSettings.SSID,
                wifiSettings.Password,
                new IPConfiguration(
                    wifiSettings.IPAddress,
                    wifiSettings.Netmask,
                    wifiSettings.GatewayAddress,
                    new string[]{wifiSettings.DNSAddress}),
                requiresDateTime: true,
                token: cs.Token);          

            if (!success)
            {
                Debug.WriteLine($"Can't connect to the network, error: {WifiNetworkHelper.Status}");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"ex: {WifiNetworkHelper.HelperException}");
                }
            }
        }
    }
}
