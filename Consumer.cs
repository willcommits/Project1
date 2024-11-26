using System.Collections.Concurrent;

namespace HelloWorld;

public class Consumer
{
    private BlockingCollection<int> _blockqueue;
    private Cache _cache;
    private int _bucketsize;
    private List<Service> _services;


    private Boolean isRunning = false;

    public Consumer(BlockingCollection<int> blockqueue,Cache cache,int bucketsize,List<Service> services)
    {
        _blockqueue = blockqueue;
        _cache = cache;
        _bucketsize = bucketsize;
        _services = services;
       
    }
    public void StartWork()
    {
        if (!isRunning)
        {
            isRunning = true;
            Thread t1 = new Thread(Work);
            t1.Start();
        }
        
    }
    public void Stop()
    
    {
        isRunning = false;
    }

    public void Work()
    {
        //int count = 0;
        while (isRunning)
        {
            if (_blockqueue.TryTake(out int value, 1000)) // Timeout of 1000 ms
            {
                _cache.PopulateCache(value,_bucketsize);
            }

            //count = 0;
            if (!_services.Any(a => a.getIsRunning()))
            {
                _cache.DisplayCacheMemoryUsage();
                Stop();
                return;
            }

            /*_services.Sum(a => a.getIsRunning() == true);
            
            for (int i = 0; i < _services.Length; i++)
            {
                if (!_services[i].getIsRunning())
                {
                    count++;
                }
            }

            if (count == _services.Length)
            {
                _cache.DisplayCacheMemoryUsage();
            }
            */

        }
      
    }
    
}