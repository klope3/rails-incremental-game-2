using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void MarkPosition(Vector3 position, Color? color = null, float duration = 1)
    {
        Color colorToUse = color ?? Color.red;
        Debug.DrawLine(position + Vector3.up, position + Vector3.down, colorToUse, duration);
        Debug.DrawLine(position + Vector3.left, position + Vector3.right, colorToUse, duration);
        Debug.DrawLine(position + Vector3.forward, position + Vector3.back, colorToUse, duration);
    }

    public static T PickRandomFromCollection<T>(IList<T> collection)
    {
        if (collection.Count == 0)
        {
            return default;
        }

        int randIndex = Random.Range(0, collection.Count);
        return collection[randIndex];
    }
}
