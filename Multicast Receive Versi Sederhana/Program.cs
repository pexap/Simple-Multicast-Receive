using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Multicast_Receive_Versi_Sederhana
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress uip = IPAddress.Parse("192.168.137.1");
            IPAddress mip = IPAddress.Parse("224.63.63.1");
            int port = 6300;

            Socket multicast = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // Deklarasi Socket UDP
            MulticastOption multicastoption = new MulticastOption(mip, uip); // Deklarasi Option Multicast

            IPEndPoint ipmulticast = new IPEndPoint(mip, port); // Deklarasi IP Multicast 
            EndPoint ipunicast = (EndPoint)new IPEndPoint(uip, port); // Deklarasi IP Unicast
            EndPoint ipremote = (EndPoint)new IPEndPoint(IPAddress.Any, 0); // Deklarasi Endpoint untuk 

            multicast.Bind(ipunicast); // Bind IP Lokal dan Port Multicast
            multicast.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastoption); // Join Multicast Group

            Console.WriteLine("Menunggu : ");

            string data;
            byte[] messageByte = new byte[100];

            while (true)
            {
                multicast.ReceiveFrom(messageByte, ref ipremote); // Socket Multicast Menerima data dari Ip Remote 
                data = Encoding.ASCII.GetString(messageByte);
                Console.WriteLine("Pesan : " + data);
            }
        }
    }
}
