namespace HelloWorld;

public class Cache
{
     Object Lock=new Object();
    private Dictionary<int, int> cache = new Dictionary<int, int>();


  
    public void populateCache(int start,int end)
    {
        lock (Lock)
        {
            for (int i = start; i < end; i++)
            {
                if (!cache.ContainsKey(i))
                {
                    cache.Add(i, i);
                }
               
            
            }
        }
        
    }
    
    
    
    
    //Does our look up 
    
}