using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Application.Models;
using Console_Application.Services;

namespace Console_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = Console.ReadLine();
            string password = Console.ReadLine();
            string apiKey = Console.ReadLine();

            HttpIGAccountService igService = new HttpIGAccountService(new IGApiAccount(username, password, apiKey));

            Console.ReadKey();
        }
    }
}
