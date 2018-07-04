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
            var videoEncoder = new Delegates.VideoEncoder(); //Publisher
            var mailService = new MailService(); //Subscriber
            var messageService = new MessageService(); //Subscriber

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
            videoEncoder.Encode(video);
            //Anonymous Methods & Lambda Expressions
            var other = new Other();
            var result = other.func(2, 3); //done with anon method
            Console.WriteLine(result);
            result = other.func2(3, 4); //done with lambda
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



            //Console.Read();
        }

        public class MailService
        {
            public void OnVideoEncoded(object source, VideoEventArgs args)
            {
                Console.WriteLine("MailService: Sending an email..." + args.Video.Title);
            }
        }

        public class MessageService
        {
            public void OnVideoEncoded(object source, VideoEventArgs args)
            {
                Console.WriteLine("MessageService: Sending message..." + args.Video.Title);
            }
        }
    }


}
