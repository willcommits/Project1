using System.Collections.Concurrent;

namespace HelloWorld
{
    public class Service
    {

        private readonly string _serviceName;
        private readonly int _lengthOfRequestedSequenceNumber;
        private readonly int _threadSleeptimeMs;
        private int _currentSequenceNumber;
        private int _startSequence = -1;
        private int _endSequence = -1;
        private readonly SequenceGenerator _seqGen;
        private BlockingCollection<Data> _blockingCollection;
        private bool isRunning = false;

        private readonly object _lock = new object();

        public int LengthOfRequestedSequenceNumber => _lengthOfRequestedSequenceNumber;

        public Service(SequenceGenerator seqGen, BlockingCollection<Data> blockingCollection,
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

                if (_startSequence == -1 || _endSequence == -1)
                {
                    var x = _seqGen.GetNextSequence(_lengthOfRequestedSequenceNumber);
                    if (x.seqExhausted)
                    {
                        StopWork();
                        return;
                    }

                    _startSequence = x.startVal;
                    _endSequence = x.endVal;
                }

                Thread t = new Thread(Work);
                t.Start();
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
                
                int sequenceTracker = 0;
                //generating my key value pair 
                Data currentData = new Data(_startSequence, _endSequence);
                for (int i = _startSequence; i <= _endSequence; i++)
                {
                    if (i % 10000 == 0)
                    {
                        //storing as key value pair Data{ start;end;}
                        _blockingCollection.Add(currentData);
                    }
                    sequenceTracker++;
                    Thread.Sleep(_threadSleeptimeMs);
                }
                
                _blockingCollection.Add(currentData);

                //checking if we have sequence numbers to allocate
                var x = _seqGen.GetNextSequence(_lengthOfRequestedSequenceNumber);
                if (x.seqExhausted)
                {
                   StopWork();
                    Console.WriteLine(" I ran out of Sequence Numbers.");
                    return;
                }
                _startSequence = x.startVal;
                _endSequence = x.endVal;
                
            }
        }

        public int GenerateHashValue(int value)
        {
            return value.GetHashCode();
        }

        public bool getIsRunning()
        {
            return isRunning;
        }
    }
}