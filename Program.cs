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
            SequenceGenerator sequenceGenerator = new SequenceGenerator(10,1000000);
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            Cache cache = new Cache();
            //we create service object,initialising the start sequence and requesting our number of sequence numbers
            Service s1 = new Service(sequenceGenerator, blockingCollection, 250, 
                10, "Web");
            s1.StartWork();
            
            Service s2 = new Service(sequenceGenerator, blockingCollection, 200, 5, "Rest");
            s2.StartWork();
           
            
            Consumer consumer=new Consumer(blockingCollection,cache);
            consumer.StartWork();


            while (true)
            {
                Console.WriteLine("do you want to see the cache?");
                Console.ReadLine();
                cache.DisplayElements();
               
             
                
           
            }
        }
    }
}