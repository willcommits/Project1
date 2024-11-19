using System;
using System.Collections.Generic;

namespace HelloWorld
{
    public class Cache
    {
        private readonly object lockObj = new object();
        private readonly HashSet<int> cache = new HashSet<int>();

        // Populates the cache with processed sequence numbers
        public void PopulateCache(int start, int end)
        {
            lock (lockObj)
            {
                for (int i = start; i < end; i++)
                {
                    if (!cache.Contains(i))
                    {
                        cache.Add(i);
                        Console.WriteLine($"Cache updated with sequence number: {i}");
                    }
                }
            }
        }

        // Displays the current cache contents
        public void DisplayCache()
        {
            lock (lockObj)
            {
                Console.WriteLine("Current Cache: " + string.Join(", ", cache));
            }
        }
    }
}