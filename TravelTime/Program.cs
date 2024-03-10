using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TravelTime
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(ConfigurationManager.AppSettings["Username"]);
            Commands.PrintCommandList();
            Commands.GetCommand();
        }
    }
}
