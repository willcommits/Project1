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
        private BlockingCollection<int> _blockingCollection;
        private bool isRunning = false;

        private readonly object _lock = new object();

        public int LengthOfRequestedSequenceNumber => _lengthOfRequestedSequenceNumber;

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
                //Console.WriteLine($"I'm the service {_serviceName} .........");
                int sequenceTracker = 0;
                for (int i = _startSequence; i <= _endSequence; i++)
                {
                    sequenceTracker++;
                    _blockingCollection.Add(i);

                    if (i % 1000 == 0)
                    {
                        _blockingCollection.Add(i);
                    }
                    
                    Thread.Sleep(_threadSleeptimeMs);
                }

                //checking if we have sequence numbers to allocate
                var x = _seqGen.GetNextSequence(_lengthOfRequestedSequenceNumber);
                if (x.seqExhausted)
                {
                   StopWork();
                    //
                    //Console.WriteLine(" I ran out of Sequence Numbers.");
                    return;
                }
                _startSequence = x.startVal;
                _endSequence = x.endVal;
                
            }
        }


        public bool getIsRunning()
        {
            return isRunning;
        }
    }
}