namespace HelloWorld;

public class Service
{
    private int lengthOfRequestedSequenceNumber; // needed sequence number
    private String ServiceName;
    private int StartSequence;// assigned start 
    private int EndSequence;// assigned end 
    
    public Service(int lengthOfRequestedSequenceNumber, String ServiceName)
    {
        this. lengthOfRequestedSequenceNumber = lengthOfRequestedSequenceNumber;
        this.ServiceName = ServiceName;
    }

    public void setStartSequence(int StartSequence)
    {
        this.StartSequence = StartSequence;
    }

    public int getStartSequence()
    {
        return this.StartSequence;
    }

    public void setEndSequence(int EndSequence)
    {
        this.EndSequence = EndSequence;
    }

    public int getEndSequence()
    {
        return this.EndSequence;
    }

    public int getLengthOfRequestedSequenceNumber()
    {
        return lengthOfRequestedSequenceNumber;
    }

    public void ProcessWork()
    {
        for (int i = StartSequence; i <= EndSequence; i++)
        {
            Console.WriteLine(ServiceName+" S "+i);
            Thread.Sleep(1000);
        }
    }
}