using System;
using System.Collections;
using System.Collections.Generic;


namespace HelloWorld
{
    public class Cache
    {
        //Value will be my data structure 
       private SortedDictionary<long, Data> cache2 = new SortedDictionary<long, Data>();
        private static int _count = 0;


        public void PopulateCache(int value,int divisor)
        {
            
            Data n=new Data()
            int quotient = value / divisor;      // This gives the '12'
            int remainder = value % divisor;     // This gives the '37163'
            
            cache2.Add(quotient,);

            
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