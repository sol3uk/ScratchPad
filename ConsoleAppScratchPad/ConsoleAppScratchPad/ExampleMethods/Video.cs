using System;
using ConsoleAppScratchPad.ExampleMethods;
using static ConsoleAppScratchPad.ExampleMethods.Delegates;

namespace ConsoleAppScratchPad.ExampleMethods
{
    public class Video
    {
        public string Title { get; set; }
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