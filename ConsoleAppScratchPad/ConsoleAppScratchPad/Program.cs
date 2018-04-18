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
            //1.3 Itteration Method Calls
            var methods = new Itteration();
            methods.PersonItterator();
            methods.PersonItteratorForEach();

            //1.4 Delegation Method Calls
            var delegates = new Delegates();
            delegates.UseDelegate();
            delegates.Multicast();
            delegates.UseDelegateLambda();

            //1.4 Event Method Calls
            var events = new Events();
            events.CreateAndRaise();
            events.CreateAndRaiseEventKeyword();
            events.CreateAndRaiseEventArgs();
            events.CreateAndRaiseCustomEventAccessors();
            events.CreateAndRaiseEventExceptions();

            //1.5 Exceptions
            var exceptions = new Exceptions();
            exceptions.ReThrowOriginalException();
            exceptions.ReThrowSameException();
            exceptions.ThrowNewException();



            Console.Read();
        }
    }


}
