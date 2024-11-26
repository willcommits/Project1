using System.Collections.Concurrent;

namespace HelloWorld
{
    public class Service
    {
        private readonly string _serviceName;
        private readonly int _lengthOfRequestedSequenceNumber;
        private readonly int _threadSleeptimeMs;

        private readonly SequenceGenerator _seqGen;
        private BlockingCollection<int> _blockingCollection;
        private bool isRunning = false;

        private readonly object _lock = new object();
        
        private Queue<int> _sequenceNumbers = new ();

        public Service(SequenceGenerator seqGen, BlockingCollection<int> blockingCollection,
            int lengthOfRequestedSequenceNumber, int threadSleeptimeMs, string serviceName)
        {
            _seqGen = seqGen;
            _blockingCollection = blockingCollection;
            _lengthOfRequestedSequenceNumber = lengthOfRequestedSequenceNumber;
            _serviceName = serviceName;
            _threadSleeptimeMs = threadSleeptimeMs;
        }


        public string GetServiceName()
        {
            return _serviceName;
        }


        public void StartWork()
        {
            if (!isRunning)
            {
                isRunning = true;

                Thread t = new Thread(Work);
                t.Start();
            }
        }

        private bool GetNextSequenceAndPopulateQueue()
        {
            var x = _seqGen.GetNextSequence(_lengthOfRequestedSequenceNumber);
            if (x.seqExhausted)
            {
                StopWork();
                return false;
            }
                    
            // Create a list to hold the numbers
            List<int> numbers = new List<int>();
            IEnumerable<int> range = Enumerable.Range((int)x.startVal, ((int)x.endVal - (int)x.startVal) + 1);
            numbers.AddRange(range);
            FisherYatesShuffle(numbers);

            foreach (var num in numbers)
            {
                _sequenceNumbers.Enqueue(num);
                if (num % 50000 == 0)
                {
                    _sequenceNumbers.Enqueue(num);
                }
            }

            return true;
        }

        private void FisherYatesShuffle(List<int> list)
        {
            Random random = new Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public void StopWork()
        {
            isRunning = false;
        }

        private void Work()
        {
            while (isRunning)
            {
                if (_sequenceNumbers.Count == 0)
                {
                    if (!GetNextSequenceAndPopulateQueue())
                    {
                        StopWork();
                        return;
                    }
                }
                
                var nextSequenceNumber = _sequenceNumbers.Dequeue();
                _blockingCollection.Add(nextSequenceNumber);
                
                Thread.Sleep(_threadSleeptimeMs);
            }
        }


        public bool getIsRunning()
        {
            return isRunning;
        }
    }
}