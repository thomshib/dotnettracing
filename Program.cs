using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnetTracing
{
    class Program
    {
        static async Task Main(string[] args)
        {

            using var listener = new CustomEventSourceListener("Microsoft-System-Net-Http");

            using(HttpClient client = new HttpClient()){

                string response = await client.GetStringAsync("http://google.com");
                Console.WriteLine($"Received response with length {response.Length}");
            }

            Console.ReadLine();
        }
    }
}