namespace HelloWorld;

public class Program
{
   static Random random = new Random();
    public static void Main(string[] args)
    {
        
        int requestedRange = 0;
        SequenceAllocator allocator=new SequenceAllocator(0);
        Cache cache=new Cache();
        
        Service s1 = new Service(1000, "Web");
        requestedRange = s1.getLengthOfRequestedSequenceNumber();
        s1.setStartSequence(allocator.getAllocatedValues()+1);//initially the zero, will now be 1
        //keeps track of the current end of the range and will need to be incremented by 1 for the next one
        allocator.SetIncrementAllocation(requestedRange);
        s1.setEndSequence(allocator.getAllocatedValues());
     
        Service s2 = new Service(100, "Rest");
        requestedRange = s2.getLengthOfRequestedSequenceNumber();
        s2.setStartSequence(allocator.getAllocatedValues()+1);
        allocator.SetIncrementAllocation(requestedRange);
        s2.setEndSequence(allocator.getAllocatedValues());

      
        
     

        while (s1.getStartSequence() < s1.getEndSequence() && s2.getStartSequence() < s2.getEndSequence())
        {
            int randomIntInRange = random.Next(1, 16); 
            cache.populateCache(s1.getStartSequence(),s1.getStartSequence()+randomIntInRange);
            Thread t1 = new Thread(()=>s1.ProcessWork(randomIntInRange));
            t1.Start();
            
            
            cache.populateCache(s2.getStartSequence(),s2.getStartSequence()+randomIntInRange);
            Thread t2 = new Thread(()=>s2.ProcessWork(randomIntInRange));
            t2.Start();

            t1.Join();
            t2.Join();
        }

    }

}