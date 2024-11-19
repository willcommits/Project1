using System;
using System.Threading;

namespace HelloWorld
{
    public class Program
    {
        static Random random = new Random();
        private static readonly object randomLock = new object();

        public static void Main(string[] args)
        {
            SequenceAllocator allocator = new SequenceAllocator(0);
            Cache cache = new Cache();

            // Initialize services with their requested sequence ranges
            Service s1 = new Service(1000, "Web");
            Service s2 = new Service(100, "Rest");

            // Allocate sequence numbers to Service 1
            int requestedRange1 = s1.LengthOfRequestedSequenceNumber;
            int start1 = allocator.GetAllocatedValues() + 1;
            allocator.SetIncrementAllocation(requestedRange1);
            s1.SetStartSequence(start1);
            s1.SetEndSequence(start1 + requestedRange1);

            // Allocate sequence numbers to Service 2
            int requestedRange2 = s2.LengthOfRequestedSequenceNumber;
            int start2 = allocator.GetAllocatedValues() + 1;
            allocator.SetIncrementAllocation(requestedRange2);
            s2.SetStartSequence(start2);
            s2.SetEndSequence(start2 + requestedRange2);

            Console.WriteLine($"{s1.GetServiceName()} allocated sequences {s1.GetStartSequence()} to {s1.GetEndSequence() - 1}");
            Console.WriteLine($"{s2.GetServiceName()} allocated sequences {s2.GetStartSequence()} to {s2.GetEndSequence() - 1}");

            // Start processing threads
            Thread t1 = new Thread(() => ProcessService(s1, cache));
            Thread t2 = new Thread(() => ProcessService(s2, cache));

            t1.Start();
            t2.Start();

            // Wait for both threads to complete
            t1.Join();
            t2.Join();

            // Display final cache contents
            cache.DisplayCache();
        }

        // Method to process a service until all sequences are processed
        public static void ProcessService(Service service, Cache cache)
        {
            while (service.GetStartSequence() < service.GetEndSequence())
            {
                int r;
                lock (randomLock)
                {
                    r = random.Next(1, 50);
                }

                service.SimulateService(cache, r);

                // Optional: Add a delay to simulate processing time
                Thread.Sleep(random.Next(10, 100));
            }

            Console.WriteLine($"{service.GetServiceName()} has completed processing.");
        }
    }
}
