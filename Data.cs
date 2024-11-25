using System.Collections;

namespace HelloWorld;

public class Data
{

    private int _size;
    private int _occupancySize;
    private BitArray _bitArray;
    public Data(int size)
    {
        _size = size;
        _bitArray = new BitArray(size, false);
        _occupancySize = 0;
    }

    public void storeValue(int value)
    {
        if (_occupancySize == _bitArray.Length)
        {
            _bitArray = null;
            return;
        }

        if (_bitArray[value] || _bitArray==null)
        {
            Console.Write("This value is already occupied, Duplicated Detected");
            return;
        }
        _bitArray[value] = true;
        _occupancySize++;
    }

}