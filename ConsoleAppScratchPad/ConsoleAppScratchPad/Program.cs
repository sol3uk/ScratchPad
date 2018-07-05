using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppScratchPad.ExampleMethods;
using static ConsoleAppScratchPad.ExampleMethods.Delegates;

namespace ConsoleAppScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
            //Other Examples
            //Video Encoder Example
            var video = new Video() { Title = "Video 1" };
            var videoEncoder = new VideoEncoder(); //Publisher
            var mailService = new MailService(); //Subscriber
            var messageService = new MessageService(); //Subscriber

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
            videoEncoder.Encode(video);
            //Anonymous Methods & Lambda Expressions
            var other = new Other();
            var result = other.addWithAnon(2, 3); //done with anon method
            Console.WriteLine(result);
            result = other.addWithLambda(3, 4); //done with lambda
            Console.WriteLine(result);
            result = other.ignoreParasPrintZero(3, 4); //done with anon method but no parameters
            Console.WriteLine(result);

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
            exceptions.UseExceptionDispatch();

            //3.1 Validate Input
            var validation = new Validation();
            var validationResult = Validation.ValidateZipCodeRegEx("1054 CH");
            Console.WriteLine(validationResult);



            //Console.Read();
        }


    }


}
