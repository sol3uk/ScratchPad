using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppScratchPad.ExampleMethods
{
    class Itteration
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

    class Delegates
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
    }

    class Events
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
                => { throw new Exception(); };

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

    class Exceptions
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
    }
}
