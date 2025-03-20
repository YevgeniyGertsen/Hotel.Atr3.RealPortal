using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int k = 0;
           while (true) { 
                //Task t = Task.Factory.StartNew(client);
                var t = Task.Run(client);
                Console.WriteLine("-> Task.Run {0}", k++);
            }
        }

        public static void  client()
        {
            using (WebClient client = new WebClient())
            {
                string url = @"https://api.satbayev.university/AttendanceRef/GetCurrentSemester";
                Url url1 = new Url(url);
                string response = client.DownloadString(url);
                Console.WriteLine("Ответ от сервера:");
                Console.WriteLine(response);
            }
        }
    }
}
