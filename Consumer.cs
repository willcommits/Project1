using System.Collections.Concurrent;

namespace HelloWorld;

public class Consumer
{
    private BlockingCollection<int> _blockqueue;
    private Cache _cache;

    private Boolean isRunning = false;

    public Consumer(BlockingCollection<int> blockqueue,Cache cache )
    {
        _blockqueue = blockqueue;
        _cache = cache;
    }

    public void Run()
    {
        if (!isRunning)
        {
            isRunning = true;
            Thread t1 = new Thread(()=>work());
        }
        
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void work()
    {
        while (isRunning)
        {
            if (_blockqueue.TryTake(out int value, 1000)) // Timeout of 1000 ms
            {
                _cache.PopulateCache(value);
            }

        }
    }
    
}