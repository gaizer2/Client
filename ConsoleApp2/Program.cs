// SocketClient.cs
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //улавливатель ошибок
            try
            {
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
        
        static void SendMessageFromSocket(int port)
        {
            
            byte[] bytes = new byte[1024];                                                          // Буфер для входящих данных
                       
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);                                   // Устанавливаем удаленную точку для сокета

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            
            sender.Connect(ipEndPoint);                                                             // Соединяем сокет с удаленной точкой
            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();                                                    //Считываем сообщение с консоли
            Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);                                           //создаем сообщение

           
            sender.Send(msg);                                                                       // Отправляем данные через сокет
            int bytesRec = sender.Receive(bytes);                                                   // Получаем ответ от сервера

            
            Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));
            SendMessageFromSocket(port);                                                            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
        }
    }
}