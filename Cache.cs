using System;
using System.Collections;
using System.Collections.Generic;


namespace HelloWorld
{
    public class Cache
    {
        //Value will be my data structure 
       private Dictionary<long, Data> cache2 = new Dictionary<long, Data>();
        private static int _count = 0;


        public void PopulateCache(int value,int divisor)
        {
            // divisor is my bucket size
            Data n = null;
          
            int quotient = value / divisor;      // This gives the '12'
            int remainder = value % divisor;     // This gives the '37163'
            
            //only create when its not there in cache
            if (!cache2.ContainsKey(quotient))
            {
                 n = new Data(divisor); //bucket size passed
                 n.storeValue(remainder);
                  cache2[quotient] = n;
            }
            else
            {
                if (cache2.TryGetValue(quotient, out n))
                {
                    if (n != null)
                    {
                        n.storeValue(remainder);
                    }
                    else
                    {
                        Console.WriteLine("Error: Retrieved null value from cache.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Key not found in cache.");
                }
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
            foreach (var kvp in cache2)
            {
                Console.WriteLine($"{kvp.Key}");
            }
        }
        public void DisplayElementsCount()
        {
            Console.Write("Number of elements in Cache: " + cache2.Count);
        }

        public void DisplayElements()
        {
            foreach (var kvp in cache2)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}