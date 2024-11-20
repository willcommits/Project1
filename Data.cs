namespace HelloWorld
{
    public class Data
    {
        private int start;
        private int end;

        public Data(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Data other)
            {
                return this.start == other.start && this.end == other.end;
            }
            return false;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(start, end);
        }

        public override string ToString()
        {
            return $"Data(start: {start}, end: {end})";
        }
    }
}