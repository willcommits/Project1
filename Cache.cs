using System;
using System.Collections.Generic;

namespace HelloWorld
{
    public class Cache
    {
       
        //private readonly HashSet<long> cache = new();
        private readonly Dictionary<long, DateTime> cache2 = new();

     
        
        public void PopulateCache(int value)
        {
          
            long memoryBefore = GC.GetTotalMemory(forceFullCollection: true);
                //cache2.Add(value, DateTime.Now);
                if (!cache2.TryAdd(value, DateTime.Now))
                {
                    Console.WriteLine($"Cache key already exists {value}");
                }


        }
        
        public void DisplayCacheMemoryUsage()
        {
            long totalMemory = GC.GetTotalMemory(forceFullCollection: true);
            Console.WriteLine($"Overall Cache memory used: {totalMemory} bytes");
        }
        // Displays the current cache contents
        public void DisplayCache()
        {
            
                Console.WriteLine($"Current Cache size: {cache2.Count}");
          
        }
    }
}