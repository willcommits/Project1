using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace HelloWorld
//2000 per second
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SequenceGenerator sequenceGenerator = new SequenceGenerator(10,100000);
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            Cache cache = new Cache();
            //we create service object,initialising the start sequence and requesting our number of sequence numbers
            Service s1 = new Service(sequenceGenerator, blockingCollection, 1000, 
                10, "Web");
            s1.StartWork();
            
            Service s2 = new Service(sequenceGenerator, blockingCollection, 500, 5, "Rest");
            s2.StartWork();
            
            Service s3 = new Service(sequenceGenerator, blockingCollection, 500, 5, "SOAP");
            s3.StartWork();

           
            
            Consumer consumer=new Consumer(blockingCollection,cache,1000);
            consumer.StartWork();


            while (true)
            {
                Console.WriteLine("do you want to see the cache?");
                Console.ReadLine();
                cache.DisplayCacheMemoryUsage();
               
             
                
           
            }
        }
    }
}