using System;
using System.Net.Sockets;
using System.Text;


namespace ConsoleClient
{
    class Program
    {
        private const int Port = 8888;
        private const string Address = "127.0.0.1";
        private const short Buffer = 1024;
        
        
        static async Task Main(string[] args)
        {
            TcpClient _client = null;
            Console.WriteLine("Используйте команду help для подсказки");
            
            try
            {
                _client = new TcpClient(Address, Port);
                NetworkStream stream = _client.GetStream();
 
                while (true)
                {
                    Console.Write("> ");
                    string message = Console.ReadLine();
                    if(message == "Q")
                        break;
                   
                    byte[] data = Encoding.UTF8.GetBytes(message);
                   
                    stream.Write(data, 0, data.Length);
 
                    
                    data = new byte[Buffer]; 
                    StringBuilder builder = new StringBuilder();
                    
                    int bytes = 0;
                    do
                    {
                        bytes = await stream.ReadAsync(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
 
                   
                    Console.WriteLine(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erorr: " + ex.Message);
            }
            finally
            {
                _client.Close();
            }
        }
    }
}