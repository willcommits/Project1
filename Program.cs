namespace HelloWorld;

public class Program
{
    public static void Main(string[] args)
    {
        int requestedRange = 0;
        SequenceAllocator allocator=new SequenceAllocator(0);
        
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
        
        
        Console.WriteLine("S1 Start:"+s1.getStartSequence());
        Console.WriteLine("S1 End:"+s1.getEndSequence());
        
        Console.WriteLine("S2 Start:"+s2.getStartSequence());
        Console.WriteLine("S2 End:"+s2.getEndSequence());
        
    }

}