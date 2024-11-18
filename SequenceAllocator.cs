namespace HelloWorld;

public class SequenceAllocator
{
    private int allocatedValues;

    public SequenceAllocator(int allocatedValues)
    {
        this.allocatedValues=allocatedValues;
    }

    public int getAllocatedValues()
    {
       return this.allocatedValues;
    }

    public void SetIncrementAllocation(int increment)
    {
        this.allocatedValues=this.allocatedValues+increment;
    }
    
    
}