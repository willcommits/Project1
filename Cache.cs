using System;
using System.Collections;
using System.Collections.Generic;



namespace HelloWorld
{
    public class Cache
    {
        private SortedDictionary<long, long> cache2 = new SortedDictionary<long, long>();
        private static int _count=0;
        
        private bool TryPeekNext(IEnumerator<KeyValuePair<long, long>> enumerator, out KeyValuePair<long, long> nextItem)
        {
            bool hasNext = enumerator.MoveNext();
            if (hasNext)
            {
                nextItem = enumerator.Current;
            }
            else
            {
                nextItem = default;
            }
            return hasNext;
        }


        
        public void PopulateCache(int value)
        {
            SortedDictionary<long, long> Updates = new SortedDictionary<long, long>();
            long headincrement = 0;
            long startdecrement = 0;
            long intervalStart=0;
            long intervalEnd=0;
            Boolean generateRange = true;


            using (var enumerator = cache2.GetEnumerator())
            {
               while(enumerator.MoveNext()){
                   
                   var kvp=enumerator.Current;
                    intervalStart = kvp.Key;
                    intervalEnd = kvp.Value;


                    if (value >= intervalStart && value <= intervalEnd)
                    {
                        Console.WriteLine("This Has been stored");
                        return;
                    }

                    startdecrement = intervalStart - 1;
                    headincrement = intervalEnd + 1;
                    if (startdecrement == value)
                    {
                        // cache2.Remove(intervalStart);
                        Updates.Add(startdecrement, intervalEnd);
                        generateRange = false;
                    }
                    else if (headincrement == value)
                    {
                        // cache2.Remove(intervalStart);
                        Updates.Add(startdecrement, intervalEnd);
                        generateRange = false;
                    }
                   // Peek at next element if available
                    KeyValuePair<long, long> nextKvp;
                    bool hasNext = TryPeekNext(enumerator, out nextKvp);
                    
                    if (hasNext)
                    {
                        long nextKey=nextKvp.Key;
                        long nextValue=nextKvp.Value;
                        //474: 474 , 475: 479

                        if (intervalStart + 1 == nextKey)
                        {
                            Updates.Add(intervalStart, nextValue);
                            //i set this to zero so i can remove it
                          Updates.Add(nextValue, 0);

                        }
                        // this is for case 480: 481 481: 481
                        else if (intervalEnd == nextKey)
                        {
                            Updates.Add(intervalStart, nextValue);
                            // REMOVE ONE OF THEM 
                            Updates.Add(intervalStart,0);
                        }
                        //this is for case 482: 486, 483: 486
                        
                        else if (intervalEnd == nextValue)
                        {
                            Updates.Add(intervalStart, nextValue);
                            //REMOVE ONE OF THEM
                             Updates.Add(nextValue, 0);
                           
                        }

                    }
                }
            }

            foreach (var kvp in Updates)
            {
                if (kvp.Value != 0)
                {
                    cache2[kvp.Key]= kvp.Value;
                }
                else
                {
                    cache2.Remove(kvp.Key);
                }
             
            }

            if (generateRange)
            {
                if (!cache2.ContainsKey(value))
                {
                    cache2.Add(value,value);
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
            Console.Write("Number of elements in Cache: "+ cache2.Count);

         
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