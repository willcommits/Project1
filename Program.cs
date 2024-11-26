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
            // Create an array of Service objects
            List<Service> services = new List<Service>();

            SequenceGenerator sequenceGenerator = new SequenceGenerator(10,10000000);
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            Cache cache = new Cache();
            //we create service object,initialising the start sequence and requesting our number of sequence numbers

            services.Add(new Service(sequenceGenerator, blockingCollection, 2000, 1, "Web")); 
            services.Add(new Service(sequenceGenerator, blockingCollection, 1000, 0, "Rest")); 
            services.Add(new Service(sequenceGenerator, blockingCollection, 500, 2, "SOAP"));

            foreach (var service in services) { service.StartWork(); }
            
            Consumer consumer=new Consumer(blockingCollection,cache,100000,services);
            consumer.StartWork();


         
        }
    }
}