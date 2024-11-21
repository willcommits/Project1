using System;
using System.Collections.Generic;



namespace HelloWorld
{
    public class Cache
    {
        private Dictionary<long, long> cache2 = new Dictionary<long, long>();
        private static int _count=0;
        
        public void PopulateCache(int value)            
        {
            
            //check ahead if a value is one more
            //check behind if value is one less
            //check ahead one more if incremented once more won't be the other start value
            //find way to combine the ranges.
            
            //delete the duplicates 
            
            cache2.Remove();
         
            
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
            Console.Write("Number of elements in Cache: "+ cache2.Count);

         
        }
    }
}