public class Singleton<T> where T : new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                InitInstance();    
            }
            
            return _instance;
        }
    }

    private static void InitInstance()
    {
        _instance = new T();
    }
}
