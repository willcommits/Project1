namespace HelloWorld
{
    public class SequenceGenerator

    {
        private int maxSequenceToAllocate = 0;
        private int allocatedSequences = 0; //helps me to know where the start of the next service will be
        private readonly object lockObj = new object();

        public SequenceGenerator(int startSEQ, int maxSEQ)
        {
            allocatedSequences = startSEQ;
            maxSequenceToAllocate = startSEQ + maxSEQ;
        }



        public SequenceGeneratorResult GetNextSequence(int requested)
        {
            lock (lockObj)
            {
                SequenceGeneratorResult result = new SequenceGeneratorResult();
                //Check if current is already Max
                if (allocatedSequences >= maxSequenceToAllocate)
                {
                    result.seqExhausted = true;
                    return result;
                }
                
                //check if there is enough to allocated based on REQ else return whats left over
                var seqLeftOver = maxSequenceToAllocate - allocatedSequences;
                if (seqLeftOver < requested)
                {
                    requested = seqLeftOver;
                }


                result.startVal = allocatedSequences; //0
                allocatedSequences += requested; //10
                result.endVal = allocatedSequences - 1; //9
                
                
                return result;
            }
        }


     
        public class SequenceGeneratorResult
        {
            public long startVal;
            public long endVal;
            public bool seqExhausted = false;
        }
    }
}