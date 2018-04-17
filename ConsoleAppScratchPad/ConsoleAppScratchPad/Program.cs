using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppScratchPad.ExampleMethods;

namespace ConsoleAppScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
            var methods = new ExampleMethods.Itteration();
            methods.PersonItterator();
            //methods.PersonItteratorForEach();

            Console.Read();
        }
    }


}
