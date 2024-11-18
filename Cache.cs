namespace HelloWorld;

public class Cache
{
    
    private Dictionary<int, int> cache = new Dictionary<int, int>();


    public void setValues(int value)
    {
        if (!cache.ContainsKey(value))
        {
         cache.Add(value, 0);   
        }
    }
    
    
    
    
    //Does our look up 
    
}