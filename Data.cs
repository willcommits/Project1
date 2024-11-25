using System.Collections;

namespace HelloWorld;

public class Data
{
    private int _occupancySize;
    private BitArray _bitArray;
    public Data(int size)
    {
        _bitArray = new BitArray(size, false);
        _occupancySize = 0;
    }

    public void storeValue(int value)
    {
        if (_occupancySize == _bitArray.Length)
        {
            _bitArray = null;
            GC.Collect(); 
            return;
        }

        if (_bitArray==null || _bitArray[value] )
        {
            Console.Write("Duplicate Detected");
            return;
        }

        if (_bitArray != null)
        {
            _bitArray[value] = true;
            _occupancySize++;
        }
    }
    
    public long GetDataSize()
    {
        if (_bitArray == null)
            return 0;

        // Size of the BitArray itself:
        // A BitArray internally uses an array of bytes to store the bits. Each byte is 8 bits.
        long bitArraySize = (long)Math.Ceiling(_bitArray.Length / 8.0);  // Estimate size in bytes.

        // Total size is the size of the BitArray + the size of the integer field _occupancySize
        long totalSize = bitArraySize + sizeof(int);

        return totalSize;
    }
    
    public override string ToString()
    {
        return string.Join(",", _bitArray);
    }

}