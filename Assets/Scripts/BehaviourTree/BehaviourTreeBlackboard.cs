using System.Collections.Generic;

public class Blackboard
{
    protected Dictionary<string, object> info;

    public object GetFromDictionary(string key)
    {
        object ret = null;
        info.TryGetValue(key, out ret);
        return ret;
    }

    public void AddToDictionary(string key, object value)
    {
        info.Add(key, value);
    }
}