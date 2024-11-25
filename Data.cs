using System.Collections;

namespace HelloWorld;

public class Data
{
    private Boolean isFull = false;
    private int _size;
    private int _occupancySize;
    private BitArray _bitArray;
    public Data(int size)
    {
        _size = size;
        _bitArray = new BitArray(size);
        _bitArray.SetAll(false);
        _occupancySize = 0;
    }

    public void storeValue(int value)
    {
        if (_occupancySize == _bitArray.Length)
        {
            isFull = true;
            _bitArray = null;
            return;
        }

        if (_bitArray[value] == true)
        {
            Console.Write("This value is already occupied, Duplicated Detected");
            return;
        }
        _bitArray[value] = true;
        _occupancySize++;
    }

}