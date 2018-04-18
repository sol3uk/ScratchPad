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
            //Itteration Method Calls
            var methods = new Itteration();
            methods.PersonItterator();
            methods.PersonItteratorForEach();

            //Delegation Method Calls
            var delegates = new Delegates();
            delegates.UseDelegate();
            delegates.Multicast();
            delegates.UseDelegateLambda();

            //Event Method Calls
            var events = new Events();
            events.CreateAndRaise();
            events.CreateAndRaiseEventKeyword();
            events.CreateAndRaiseEventArgs();
            events.CreateAndRaiseCustomEventAccessors();
            events.CreateAndRaiseEventExceptions();

            Console.Read();
        }
    }


}
