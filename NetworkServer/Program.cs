using System;
using System.Net;
using System.Threading;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkLibrary.DataTransferObjects;
using NetworkLibrary.NetworkMessages;
using NetworkServer.Networking;

namespace NetworkServer
{
    class Program
    {
        private const int SERVER_PORT = 5400;

        static void Main(string[] args)
        {
            var dataStorage = new DataStorage();
            dataStorage.Save(new PersonDto
            {
                Id = 1,
                FirstName = "Dolores",
                LastName = "Abernathy",
                Gender = "Female",
                Birthday = new DateTime(2086, 2, 5)
            });
            dataStorage.Save(new PersonDto
            {
                Id = 2,
                FirstName = "Robert",
                LastName = "Ford",
                Gender = "Male",
                Birthday = new DateTime(2058, 6, 23)
            });
            dataStorage.Save(new PersonDto
            {
                Id = 3,
                FirstName = "Maeve",
                LastName = "Millay",
                Gender = "Female",
                Birthday = new DateTime(2069, 3, 28)
            });
            dataStorage.Save(new PersonDto
            {
                Id = 4,
                FirstName = "Bernard",
                LastName = "Lowe",
                Gender = "Male",
                Birthday = new DateTime(2068, 2, 15)
            });
            var messageHandler = new MessageHandler(dataStorage);


            //Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<HelloMessage>(typeof(HelloMessage).Name, PrintIncomingMessage);
            NetworkComms.AppendGlobalIncomingUnmanagedPacketHandler(HandleUnmanaged);
            //            NetworkComms.EnableLogging();
            messageHandler.Load();

            //Start listening for incoming connections
            var desiredLocalEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, SERVER_PORT);
            Connection.StartListening(ConnectionType.TCP, desiredLocalEndPoint);

            //Print out the IPs and ports we are now listening on
            Console.WriteLine("Server listening for TCP connection on:");
            foreach (var endPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                var localEndPoint = (IPEndPoint)endPoint;
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
            }

            //Let the user close the server
            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);

            //We have used NetworkComms so we should ensure that we correctly call shutdown
            NetworkComms.Shutdown();
        }

        private static void HandleUnmanaged(PacketHeader packetheader, Connection connection, byte[] incomingobject)
        {
            try
            {
                var data = System.Text.Encoding.UTF8.GetString(incomingobject);
                Console.WriteLine("Unmanaged message recieved: " + data);
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION : " + e.GetType() + " " + e.Message);
            }
        }

        private static void PrintIncomingMessage(PacketHeader header, Connection connection, HelloMessage message)
        {
            try
            {
                Console.WriteLine("\nA HelloMessage was received from " + connection.ToString() + " which said '" +
                                  message.Message +
                                  "'.");
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION : " + e.GetType() + " " + e.Message);
            }
        }
    }
}