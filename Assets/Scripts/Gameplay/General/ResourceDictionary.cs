using UnityEngine;
using System.Collections.Generic;

public class ResourceDictionary<T> where T : Object, IHasId
{
    private Dictionary<string, T> dict;

    public ResourceDictionary(string resourceDirectory)
    {
        dict = new Dictionary<string, T>();
        T[] allItems = Resources.LoadAll<T>(resourceDirectory);
        foreach (T item in allItems)
        {
            dict.Add(item.Id, item);
        }
    }

    public bool TryGetItem(string id, out T item)
    {
        return dict.TryGetValue(id, out item);
    }
}
