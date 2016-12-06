using System;
using System.Configuration;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkLibrary.DataTransferObjects;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient.Networking
{
    public class NetworkManager
    {
        private readonly string _ipAddress;
        private readonly int _serverPort;
        private Dispatcher UiDispatcher => Application.Current.Dispatcher;

        public EventHandler<PersonAddedEvent> PersonAdded;

        public NetworkManager()
        {
            //            _ipAddress = ConfigurationManager.AppSettings["SERVER_IP"];
            //            _serverPort = Convert.ToInt32(ConfigurationManager.AppSettings["SERVER_PORT"]);

            _ipAddress = "127.0.0.1";
            _serverPort = 5400;
            Console.WriteLine("CONFIG: " + _ipAddress + ":" + _serverPort);
            NetworkComms.AppendGlobalIncomingPacketHandler<StorePersonMessage>(typeof(StorePersonMessage).Name,
                Network_PersonAdded);
        }

        private void Network_PersonAdded(PacketHeader packetheader, Connection connection,
            StorePersonMessage incomingobject)
        {
            Console.WriteLine("Recieved on thread:" + Thread.CurrentThread.Name);
            UiDispatcher.BeginInvoke(new Action(() =>
            {
                Console.WriteLine("Dispatched to thread:" + Thread.CurrentThread.Name);
                PersonAdded?.Invoke(this, new PersonAddedEvent(incomingobject.Person));
            }));
        }

        public string SendMessage(object message)
        {
            try
            {
                Console.WriteLine("Sending on thread:" + Thread.CurrentThread.Name);
                NetworkComms.SendObject(message.GetType().Name, _ipAddress, _serverPort, message);
                return "Sent message.";
            }
            catch (ConnectionSetupException e)
            {
                return "Message failed, could not connect to server";
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION:" + e.GetType().Name + " " + e.Message);
                return "EXCEPTION: " + e.GetType().Name + " " + e.Message;
            }
            return "";
        }
    }

    public class PersonAddedEvent : EventArgs
    {
        public PersonDto Person { get; set; }

        public PersonAddedEvent(PersonDto person)
        {
            Person = person;
        }
    }
}