using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using QBid.APILog;
using QBid.DependencyServices;
using QBid.iOS.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(IPAddressManager))]
namespace QBid.iOS.DependencyServices
{
    public class IPAddressManager: IIPAddressManager
    {
        public string GetIPAddress()
        {
            String ipAddress = string.Empty;
            try
            {
                foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
                {

                    //if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    //                   netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    //{
                        foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                ipAddress = Convert.ToString(addrInfo.Address);

                            }
                        }
                   // }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
               
            }
            return ipAddress;
        }
        public IPAddressManager()
        {
        }
    }
}
