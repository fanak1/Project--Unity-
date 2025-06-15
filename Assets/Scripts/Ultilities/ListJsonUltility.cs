using UnityEngine;

using System.Collections.Generic;
using System.IO;

public class ListJsonUtility
{
    [System.Serializable]
    private class ListWrapper<T>
    {
        public List<T> items;
    }

    public static List<T> FromJson<T>(string json)
    {
        var wrapper = JsonUtility.FromJson<ListWrapper<T>>(json);
        return wrapper?.items ?? new List<T>();
    }

    public static string ToJson<T>(List<T> list)
    {
        var wrapper = new ListWrapper<T> { items = list };
        return JsonUtility.ToJson(wrapper, true);
    }
}

