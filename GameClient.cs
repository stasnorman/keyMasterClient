using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KeyLogerClient
{
    public class GameClient
    {
        public void Connect(string server, int port)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();

                // Получение зашифрованного кода от сервера
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("Server: " + Encoding.ASCII.GetString(buffer, 0, bytes));

                while (true)
                {
                    Console.WriteLine("Enter your guess:");
                    string message = Console.ReadLine();
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    bytes = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytes);
                    Console.WriteLine("Server: " + response);

                    if (response.Contains("Correct!"))
                    {
                        break;
                    }
                }

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
