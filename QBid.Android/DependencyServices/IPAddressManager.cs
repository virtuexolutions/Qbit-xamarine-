using System;
using System.Net;
using QBid.APILog;
using QBid.DependencyServices;
using QBid.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(IPAddressManager))]
namespace QBid.Droid.DependencyServices
{
    public class IPAddressManager: IIPAddressManager
    {
        public string GetIPAddress()
        {
            try
            {
                IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());

                if (adresses != null && adresses[0] != null)
                {
                    return Convert.ToString(adresses[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        public IPAddressManager()
        {
        }
    }
}
