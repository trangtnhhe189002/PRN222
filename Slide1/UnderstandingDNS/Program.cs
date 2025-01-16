namespace UnderstandingDNS
{
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new string('*', 30));

            // Get DNS host entry for a domain
            var domainEntry = Dns.GetHostEntry("www.contoso.com");
            Console.WriteLine(domainEntry.HostName);
            foreach (var ip in domainEntry.AddressList)
            {
                Console.WriteLine(ip);
            }

            Console.WriteLine(new string('*', 30));

            // Get DNS host entry for an IP address
            var domainEntryByAddress = Dns.GetHostEntry("127.0.0.1");
            Console.WriteLine(domainEntryByAddress.HostName);
            foreach (var ip in domainEntryByAddress.AddressList)
            {
                Console.WriteLine(ip);
            }

            Console.ReadLine();
        }
    }

}
