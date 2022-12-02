using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleAppScratchPad.ExampleMethods
{
    public class Itteration
    {
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        #region Custom Version of Listing 1-72
        public void PersonItteratorForEach()
        {
            var people = new List<Person>
            {
                new Person() { FirstName = "Ben", LastName = "Ashley" },
                new Person() { FirstName = "Nathan", LastName = "Ashley" },
            };

            List<Person>.Enumerator e = people.GetEnumerator();

            try
            {
                foreach (Person p in people)
                {
                    p.FirstName = "bob";
                    Console.WriteLine(p.FirstName + p.LastName);
                }
            }
            finally
            {
                System.IDisposable d = e as System.IDisposable;
                if (d != null) d.Dispose();
            }
        }
        #endregion

        #region Listing 1-73
        public void PersonItterator()
        {
            var people = new List<Person>
            {
                new Person() { FirstName = "Ben", LastName = "Ashley" },
                new Person() { FirstName = "Nathan", LastName = "Ashley" },
            };

            List<Person>.Enumerator e = people.GetEnumerator();

            try
            {
                Person v;
                while (e.MoveNext())
                {
                    v = e.Current;
                    Console.WriteLine(v.FirstName + v.LastName);
                }

            }
            finally
            {
                System.IDisposable d = e as System.IDisposable;
                if (d != null) d.Dispose();
            }
        }
        #endregion
    }

    public class Delegates
    {
        #region Listing 1-75
        public delegate int Calculate(int x, int y);

        public int Add(int x, int y) { return x + y; }
        public int Multiply(int x, int y) { return x * y; }

        public void UseDelegate()
        {
            Calculate calc = Add;
            Console.WriteLine(calc(3, 4));

            calc = Multiply;
            Console.WriteLine(calc(3, 4));
        }
        #endregion

        #region Listing 1-76
        public void MethodOne()
        {
            Console.WriteLine("MethodOne");
        }
        public void MethodTwo()
        {
            Console.WriteLine("MethodTwo");
        }

        public delegate void Del();

        public void Multicast()
        {
            Del d = MethodOne;
            d += MethodTwo;

            d();
        }
        #endregion

        #region Listing 1-79
        public void UseDelegateLambda()
        {
            Calculate calc = (x, y) => x + y;
            Console.WriteLine(calc(3, 4));

            calc = (x, y) => x * y;
            Console.WriteLine(calc(3, 4));
        }
        #endregion

        #region Video Encoding Example

        public class VideoEventArgs : EventArgs
        {
            public Video Video { get; set; }
        }

        public class VideoEncoder
        {
            // 1 Define delegate
            // 2 Define an event based on the delegate
            // 3 Raise the event

            public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args);

            public event VideoEncodedEventHandler VideoEncoded;


            public void Encode(Video video)
            {
                Console.WriteLine("Encoding video...");
                Thread.Sleep(3000);

                OnVideoEncoded(video);
            }

            protected virtual void OnVideoEncoded(Video video)
            {
                VideoEncoded?.Invoke(this, new VideoEventArgs() { Video = video });
            }
        }

        #endregion
    }

    public class Events
    {
        #region Listing 1-82
        public class Pub
        {
            public Action OnChange { get; set; }

            public void Raise()
            {
                //Listings Example
                //if (OnChange != null)
                //{
                //    OnChange();
                //}

                //Simplified
                OnChange?.Invoke();
                Console.WriteLine("OnChange Invoked");

            }
        }

        public void CreateAndRaise()
        {
            Pub p = new Pub();
            p.OnChange += () => Console.WriteLine("Event raised to method 1");
            p.OnChange += () => Console.WriteLine("Event raised to method 2");
            p.Raise();
        }
        #endregion

        #region Listing 1-83
        public class PubEventKeyword
        {
            public event Action OnChange = delegate { };

            public void Raise()
            {
                OnChange();
                Console.WriteLine("OnChange Invoked");
            }
        }

        public void CreateAndRaiseEventKeyword()
        {
            PubEventKeyword p = new PubEventKeyword();
            p.OnChange += () => Console.WriteLine("Event raised to method 1");
            p.OnChange += () => Console.WriteLine("Event raised to method 2");
            p.Raise();
        }
        #endregion

        #region Listing 1-84
        public class MyArgs : EventArgs
        {
            public MyArgs(int value)
            {
                Value = value;
            }
            public int Value { get; set; }
        }

        public class PubEventArgs
        {
            public event EventHandler<MyArgs> OnChange = delegate { };

            public void Raise()
            {
                OnChange(this, new MyArgs(42));
                Console.WriteLine("OnChange Invoked");
            }
        }

        public void CreateAndRaiseEventArgs()
        {
            PubEventArgs p = new PubEventArgs();
            p.OnChange += (sender, e)
                => Console.WriteLine("Event raised: {0}", e.Value);
            p.Raise();
        }
        #endregion

        #region Listing 1-85
        public class PubCustomEventAccessors
        {
            private event EventHandler<MyArgs> onChange = delegate { };
            public event EventHandler<MyArgs> OnChange
            {
                add
                {
                    lock (onChange)
                    {
                        onChange += value;
                        Console.WriteLine("Add Subscriber Invoked");
                    }
                }
                remove
                {
                    lock (onChange)
                    {
                        onChange -= value;
                        Console.WriteLine("Remove Subscriber Invoked");
                    }
                }
            }

            public void Raise()
            {
                onChange(this, new MyArgs(420));
                Console.WriteLine("onChange Invoked");
            }
        }

        public void CreateAndRaiseCustomEventAccessors()
        {
            PubCustomEventAccessors p = new PubCustomEventAccessors();
            p.OnChange += (sender, e)
                => Console.WriteLine("Event raised: {0}", e.Value);
            p.Raise();
        }
        #endregion

        #region Listing 1-87
        public class PubEventExceptions
        {
            public event EventHandler OnChange = delegate { };
            public void Raise()
            {
                var exceptions = new List<Exception>();

                foreach (Delegate handler in OnChange.GetInvocationList())
                {
                    try
                    {
                        handler.DynamicInvoke(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }

                if (exceptions.Any())
                {
                    throw new AggregateException(exceptions);
                }
            }
        }

        public void CreateAndRaiseEventExceptions()
        {
            PubEventExceptions p = new PubEventExceptions();

            p.OnChange += (sender, e)
                => Console.WriteLine("Subscriber 1 called");

            p.OnChange += (sender, e)
                =>
            { throw new Exception(); };

            p.OnChange += (sender, e)
                => Console.WriteLine("Subscriber 3 called");

            try
            {
                p.Raise();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions.Count);
            }
        }
        #endregion

    }

    public class Exceptions
    {
        //Generic Exception Log
        private void Log(Exception logEx)
        {
            Console.WriteLine("Exception Logged");
        }

        #region Listing 1-94
        public static string OpenAndParse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName", "Filename is required");

            return File.ReadAllText(fileName);
        }
        #endregion

        #region Listing 1-95
        public void ReThrowOriginalException()
        {
            try
            {
                OpenAndParse(null);
            }
            catch (Exception logEx)
            {
                Log(logEx);
                throw; //This re-throws the original Exception without altering anything, this preserves the stack trace.
            }
        }

        #endregion

        #region Custom Version of Listing 1-95
        public void ReThrowSameException()
        {
            try
            {
                OpenAndParse(null);
            }
            catch (Exception logEx)
            {
                Log(logEx);
                throw logEx; //This re-throws the same Exception but inside the catch block, it resets the stack trace and makes it HARDER to debug...
            }
        }
        #endregion

        #region Listing 1-96
        public void ThrowNewException()
        {
            try
            {
                OpenAndParse(null);
            }
            catch (Exception ex)
            {
                //This throws a brand new exception whislt pointing to the existing exception, preserving the stack trace
                //This makes it EASIER to debug, because you can provide additional info about what happened.
                throw new ArgumentNullException("Error getting file", ex);
            }
        }
        #endregion

        #region Listing 1-97
        public void UseExceptionDispatch()
        {
            ExceptionDispatchInfo possibleException = null;

            try
            {
                string s = Console.ReadLine();
                int.Parse(s);
            }
            catch (FormatException ex)
            {
                possibleException = ExceptionDispatchInfo.Capture(ex);
            }

            if (possibleException != null)
            {
                possibleException.Throw();
            }
        }
        #endregion

        #region Listing 1-98
        //Example of implementing custom exception
        [Serializable]
        public class OrderProcessingException : Exception, ISerializable
        {
            public OrderProcessingException(int orderId)
            {
                OrderId = orderId;
                this.HelpLink = "Link to more info";
            }
            public OrderProcessingException(int orderId, string message) : base(message)
            {
                OrderId = orderId;
                this.HelpLink = "Link to more info";
            }

            public OrderProcessingException(int orderId, string message, Exception innerException) : base(message, innerException)
            {
                OrderId = orderId;
                this.HelpLink = "Link to more info";
            }

            protected void EntityOperationException(SerializationInfo info, StreamingContext context)
            {
                OrderId = (int)info.GetValue("OrderId", typeof(int));
            }

            public int OrderId { get; set; }

            public int entityId { get; set; }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
                info.AddValue("entityId", entityId, typeof(int));
            }
        }

        public void ThrowTestExceptions()
        {
            throw new OrderProcessingException(5);
            throw new OrderProcessingException(5, "Custom Error Message");
            throw new OrderProcessingException(5, "Custom Error Message", new Exception().InnerException);
        }

        #endregion
    }

    public class manipulateStrings
    {
        #region Listing 2-89 StringWriter and XmlWriter
        public string makeSomeXML()
        {
            var stringWriter = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(stringWriter))
            {
                writer.WriteStartElement("book");
                writer.WriteElementString("price", "19.95");
                writer.WriteEndElement();
                writer.Flush();
            }
            return stringWriter.ToString();
            }
        #endregion
    }

    public class Validation
    {
        #region Listing 3-10 Validate Dutch Zip Code
        public static bool ValidateZipCodeRegEx(string zipCode)
        {
            Match match = Regex.Match(zipCode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$", RegexOptions.IgnoreCase);
            return match.Success;
        }
        #endregion

        #region Listing 3-11 Remove Spaces
        public void RegexReplaceSpaces()
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);

            string input = "1 2 3 4    5";
            string result = regex.Replace(input, " ");

            Console.WriteLine(result);
        }
        #endregion

        #region Listing 3-12 Is This JSON?
        public static bool IsJson(string input)
        {
            //Only checking the start and end of the string, not if the input can be parsed as JSON.
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                || input.StartsWith("[") && input.EndsWith("]");
        }

        #endregion
    }

    public class Other
    {
        public Func<int, int, int> addWithAnon = delegate (int x, int y)
        {
            return x + y;
        };

        public Func<int, int, int> addWithLambda = (x, y) => x + y;

        public Func<int, int, int> ignoreParasPrintZero = delegate
        {
            return 0;
        };
    }
}
