namespace UDPClientApp
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    class Program
    {
        static void ConnectServer(string host, int port)
        {
            UdpClient client = new UdpClient();
            IPAddress address = IPAddress.Parse(host);
            IPEndPoint remoteEndpoint = new IPEndPoint(address, port);

            string message;
            int count = 0;
            bool done = false;

            Console.Title = "UDP Client";

            try
            {
                Console.WriteLine(new string('*', 40));
                client.Connect(remoteEndpoint);

                while (!done)
                {
                    message = $"Message {++count:D2}";
                    byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                    client.Send(sendBytes, sendBytes.Length);
                    Console.WriteLine($"Sent: {message}");

                    if (count == 10) // broadcast 10 messages
                    {
                        done = true;
                        Console.WriteLine("Done.");
                    }

                    Thread.Sleep(2000); // wait for 2 seconds between messages
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Close();
            }
        }

        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 11000;
            ConnectServer(host, port);
            Console.Read();
        }
    }

}
