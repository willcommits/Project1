using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SequenceGenerator sequenceGenerator = new SequenceGenerator(10000);
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            //we create service object,initialising the start sequence and requesting our number of sequence numbers
            Service s1 = new Service(sequenceGenerator, blockingCollection, 1000, 
                1000, "Web");
            s1.StartWork();
            s1.StartWork();
            
            Service s2 = new Service(sequenceGenerator, blockingCollection, 2000, 500, "Rest");
            s2.StartWork();
            Console.ReadLine();
        }
    }
}