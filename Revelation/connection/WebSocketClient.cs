
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Revelation.connection
{
    public static class WebSocketClient
    {
        private static WebSocket ws;

        public static void Connection()
        {
            var url = Settings.host + ":" + Settings.port;
            ws = new WebSocket(url);

            //ws.WaitTime = TimeSpan.FromMinutes(100);
            ws.OnOpen += (sender, e) => {
                Console.WriteLine("Connecté");
                ws.Send("Hi, there!");
                Console.WriteLine("Message envoyé");

            };
   
            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Camille says OnMessage: " + e.Data);
            };
                

            ws.OnError += (sender, e) =>
            {
                Console.WriteLine("Camille says OnError: " + e.Message);
            };

            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("Camille says OnError Close: " + e.Code + ":" + e.Reason);
            };
            

            ws.Connect();
        

        }

        private static Task OnError(ErrorEventArgs errorEventArgs)
        {
            Console.Write("Error: {0}, Exception: {1}", errorEventArgs.Message, errorEventArgs.Exception);
            return Task.FromResult(0);
        }

        private static Task OnMessage(MessageEventArgs messageEventArgs)
        {
            //.Text.ReadToEnd()
            Console.Write("Message received: {0}", messageEventArgs.Data.ToString());
            return Task.FromResult(0);
        }
        private static void WsOnOnClose(object sender, CloseEventArgs closeEventArgs)
        {
            Console.WriteLine("Close 1");
            if (!closeEventArgs.WasClean)
            {
                Console.WriteLine("Close 2");
                if (!ws.IsAlive)
                {
                    Console.WriteLine("Close 3");
                    Thread.Sleep(1000);
                    ws.Connect();
                }
            }
        }
    }
}
