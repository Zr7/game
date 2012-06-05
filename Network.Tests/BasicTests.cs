﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Network;
using System.Net;
using System.Threading;

namespace Network.Tests
{
    [TestFixture]
    public class BasicTests
    {

        [Test]
        public void TestResolveUtilityMethod()
        {
            var addressesToLocalHost = Utils.ResolveHost("localhost");
            foreach (var addr in addressesToLocalHost)
            {
                Console.WriteLine(addr);
            }
        }

        [Test]
        public void TestConnectAClientToAServer()
        {
            var testClient = new Client();
            var testServer = new Server();

            testServer.StartListening(991);

            testClient.BeginConnect("127.0.0.1", 991);

            Thread.Sleep(5000);
        }
    }
}