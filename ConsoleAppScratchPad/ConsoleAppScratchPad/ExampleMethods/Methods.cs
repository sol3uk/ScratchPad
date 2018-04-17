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




    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
