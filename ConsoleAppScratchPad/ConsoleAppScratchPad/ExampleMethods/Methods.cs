using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppScratchPad.ExampleMethods
{
    class Itteration
    {
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
        #region 1-82
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

        #region 1-83
        public class PubEventKeyword
        {
            public event Action OnChange = delegate { };

            public void Raise()
            {
                OnChange();
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
    }


    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
