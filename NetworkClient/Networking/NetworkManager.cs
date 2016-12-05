using System;
using System.Configuration;
using System.Resources;
using NetworkCommsDotNet;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient.Networking
{
    public class NetworkManager
    {
        private readonly string _ipAddress;
        private readonly int _serverPort;

        public NetworkManager()
        {
            //            _ipAddress = ConfigurationManager.AppSettings["SERVER_IP"];
            //            _serverPort = Convert.ToInt32(ConfigurationManager.AppSettings["SERVER_PORT"]);

            _ipAddress = "127.0.0.1";
            _serverPort = 5400;
            Console.WriteLine("CONFIG: " + _ipAddress + ":" + _serverPort);
        }

        public string SendMessage(string message)
        {
            try
            {
                NetworkComms.SendObject("Message", _ipAddress, _serverPort, message);
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
}