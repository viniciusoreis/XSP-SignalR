using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            IHubProxy _hub;
            string url = @"http://localhost:63853/";
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("ChatHub");
            connection.Start().Wait();

            Console.WriteLine("Nome: ");
            var nome = Console.ReadLine();

            _hub.On<string, string>("broadcastMessage", (name, message) => Console.WriteLine(name + " : " + message));


            Enviar(_hub, nome);
        }

        private static void Enviar(IHubProxy _hub, string nome)
        {
            var mensagem = Console.ReadLine();
            _hub.Invoke("Send", nome, mensagem).Wait();

            Enviar(_hub, nome);
        }

    }
}
