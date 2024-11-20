using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace HelloWorld
{
    public class Cache
    {
        
        private readonly Dictionary<Data, DateTime> cache2 = new();
        public void PopulateCache(Data value)
        {
            if (!cache2.TryAdd(value, DateTime.Now))
            {
                Console.WriteLine($"Cache key already exists: {value}");
            }
            else
            {
                Console.WriteLine($"Added to cache: {value}");
            }
        }
        
        //memory usage of the cache 
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