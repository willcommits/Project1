namespace HelloWorld
{
    public class Service
    {
     
        private readonly int _lengthOfRequestedSequenceNumber;
        private string _serviceName;
        private int _startSequence; 
        private int _endSequence;   

     
        public int LengthOfRequestedSequenceNumber => _lengthOfRequestedSequenceNumber;

     
        public Service(int lengthOfRequestedSequenceNumber, string serviceName)
        {
            _lengthOfRequestedSequenceNumber = lengthOfRequestedSequenceNumber;
            _serviceName = serviceName;
        }

       
        public void SetStartSequence(int startSequence)
        {
            _startSequence = startSequence;
        }

        public int GetStartSequence()
        {
            return _startSequence;
        }

        public void SetEndSequence(int endSequence)
        {
            _endSequence = endSequence;
        }

        public int GetEndSequence()
        {
            return _endSequence;
        }

        public string GetServiceName()
        {
            return _serviceName;
        }

        // Simulates processing sequence numbers
        public void SimulateService(Cache cache, int value)
        {
            int processStart, processEnd;

            lock (this)
            {
                if (_startSequence >= _endSequence)
                {
                    Console.WriteLine($"{_serviceName}: No more sequences to process.");
                    return;
                }

                processStart = _startSequence;
                processEnd = Math.Min(_startSequence + value, _endSequence);
                _startSequence = processEnd; 
            }

            Console.WriteLine($"{_serviceName} processing sequences {processStart} to {processEnd - 1}");
            cache.PopulateCache(processStart, processEnd);
        }
    }
}
