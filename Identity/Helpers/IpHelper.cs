using System.Net;

namespace Identity.Helpers
{
    public class IpHelper
    {
        public static string GetIpAdress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }
    }
}
