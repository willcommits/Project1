using System;
using System.Collections.Generic;

namespace HelloWorld
{
    public class Cache
    {
        private readonly object lockObj = new object();
        private readonly HashSet<int> cache = new HashSet<int>();

     
        
        public void PopulateCache(int value)
        {
            lock (lockObj)
            {
         
                cache.Add(value);
                
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