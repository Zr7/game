﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network;
using System.Net;
using System.Threading;
using System.Net.Sockets;

// Greate page about about events and delegates in C#
//http://www.codeproject.com/Articles/20550/C-Event-Implementation-Fundamentals-Best-Practices

// Using an asynchronous client socket
// http://msdn.microsoft.com/en-us/library/bbx2eya8.aspx

namespace Network
{
    public class ConnectEventArgs : EventArgs
    {
        public bool Connected { get; private set; }

        public ConnectEventArgs(bool connected)
        {
            Connected = connected;
        }
    }



    public class Client
    {
        Socket ClientSocket { get; set; }

        public bool Connected { get; private set; }
 
        public event OnConnectionChangedHandler OnConnectionChanged;
        public delegate void OnConnectionChangedHandler(Object sender, ConnectEventArgs e);

        // Asynchronous connect to host.
        public void BeginConnect(string hostName, int port)
        {
            var ipAddress = IPAddress.Parse(hostName);
            var ipEndPoint = new IPEndPoint(ipAddress, port);

            if (ClientSocket != null)
            {
                throw new NotImplementedException("TODO: properly close any lingering socket, if connecting multiple times with the same client instance");
            }

            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Client::BeginConnect");
            ClientSocket.BeginConnect(ipEndPoint, EndConnect, null);
        }

        public void BeginDisconnect()
        {

        }

        public void EndConnect(IAsyncResult ar)
        {
            ClientSocket.EndConnect(ar);
            OnConnected();
        }

        public void Send(String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            ClientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        private void SendCallback(IAsyncResult ar)
        {
            // Complete sending the data to the remote device.
            int bytesSent = ClientSocket.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server.", bytesSent);
        }

        // Event raising code
        private void OnConnected()
        {
            Connected = true;
            if (this.OnConnectionChanged != null)
            {
                OnConnectionChanged(this, new ConnectEventArgs(true));
            }
        }

        private void OnDisconnected()
        {
            Connected = false;
            if (this.OnConnectionChanged != null)
            {
                OnConnectionChanged(this, new ConnectEventArgs(false));
            }
        }

    }
}
