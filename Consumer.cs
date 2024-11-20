using System.Collections.Concurrent;

namespace HelloWorld;

public class Consumer
{
    private BlockingCollection<int> _blockqueue;
    private Cache _cache;
    private Service _s1;
    private Service _s2;

    private Boolean isRunning = false;

    public Consumer(BlockingCollection<int> blockqueue,Cache cache,Service s1,Service s2 )
    {
        _blockqueue = blockqueue;
        _cache = cache;
        _s1 = s1;
        _s2 = s2;
    }

    public void StartWork()
    {
        if (!isRunning)
        {
            isRunning = true;
            Thread t1 = new Thread(work);
            t1.Start();
        }
        
    }

    public void Stop()
    
    {
        isRunning = false;
    }

    public void work()
    {
        //I made an assumption that if both the services are still running and as we constanly monitor our blocking queue will always retrieving the value if one is ever stored
        //in the queue
        while (isRunning)
        {
     
            if (_blockqueue.TryTake(out int value, 1000)) // Timeout of 1000 ms
            {
                _cache.PopulateCache(value
                );
            }

        }

       Stop();
       _cache.DisplayCache();
    }
    
}