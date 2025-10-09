using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CollectionExtentions
{
    public static T GetRandomElement<T>(this IEnumerable<T> collection)
    {
        var randomIndex = Random.Range(0, collection.Count());
        return collection.ElementAt(randomIndex);
    }
}