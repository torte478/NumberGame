using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var entity = new NumberGame.Class1();
            Console.WriteLine("Hello, world!" + entity.Method1(4));
        }
    }
}
