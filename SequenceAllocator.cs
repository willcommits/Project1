namespace HelloWorld
{
    public class SequenceAllocator
    {
        private int allocatedValues;
        private readonly object lockObj = new object();

        public SequenceAllocator(int allocatedValues)
        {
            this.allocatedValues = allocatedValues;
        }

        public int GetAllocatedValues()
        {
            lock (lockObj)
            {
                return this.allocatedValues;
            }
        }

        public void SetIncrementAllocation(int increment)
        {
            lock (lockObj)
            {
                this.allocatedValues += increment;
            }
        }
    }
}