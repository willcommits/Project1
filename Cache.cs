using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


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
                 n.storeValue(remainder, quotient);
                  cache2[quotient] = n;
            }
            else
            {
                if (cache2.TryGetValue(quotient, out n))
                {
                    if (n != null)
                    {
                        n.storeValue(remainder,quotient);
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
            Console.WriteLine($"Overall memory used: {totalMemory} bytes");

            long totalCacheSize = 0;
            long dictionaryMemorySize = 0;

            // Calculate the memory size of cache2 (Dictionary)
            foreach (var kvp in cache2)
            {
                // Memory used by the Dictionary itself (key-value pair)
                dictionaryMemorySize += sizeof(long); // Size of long (8 bytes for key)
                dictionaryMemorySize += IntPtr.Size; // Size of reference to Data object (4 bytes on 32-bit or 8 bytes on 64-bit)
                if (kvp.Value != null)
                {
                    totalCacheSize += kvp.Value.GetDataSize(); // Add the size of each Data object
                }
            }

            // Convert bytes to megabytes
            double dictionaryMemorySizeMB = dictionaryMemorySize / 1024.0 / 1024.0;
            double totalCacheSizeMB = totalCacheSize / 1024.0 / 1024.0;
            double totalMemoryUsedMB = (dictionaryMemorySize + totalCacheSize) / 1024.0 / 1024.0;

// Display memory usage in MB
            Console.WriteLine($"Memory used by the dictionary structure: {dictionaryMemorySizeMB:F2} MB");
            Console.WriteLine($"Memory used by the data objects in cache: {totalCacheSizeMB:F2} MB");
            Console.WriteLine($"Total memory used by cache2: {totalMemoryUsedMB:F2} MB");

            
        }




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