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
            SequenceGenerator sequenceGenerator = new SequenceGenerator(3000);
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            Cache cache = new Cache();
            //we create service object,initialising the start sequence and requesting our number of sequence numbers
            Service s1 = new Service(sequenceGenerator, blockingCollection, 1000, 
                10, "Web");
            s1.StartWork();
            
            Service s2 = new Service(sequenceGenerator, blockingCollection, 2000, 5, "Rest");
            s2.StartWork();
           
            
            Consumer consumer=new Consumer(blockingCollection,cache,s1,s2);
            consumer.StartWork();

            while (true)
            {
                Console.WriteLine("Do you want to print the data?");
                Console.ReadLine();
                cache.DisplayCache();
                cache.DisplayCacheMemoryUsage();
            }
            
          
            
            
        }
    }
}