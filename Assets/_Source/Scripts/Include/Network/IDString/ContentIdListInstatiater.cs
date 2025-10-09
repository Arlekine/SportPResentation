using System.Collections.Generic;
using UnityEngine;

public abstract class ContentIdListInstatiater<T1, T2> : ContentIDList<T1, T2>
    where T1 : Object
    where T2 : IDHolder
{
    public override T1 Get(string ID)
    {
        var prefab = base.Get(ID);
        return Instantiate(prefab);
    }

    public override IEnumerable<T1> GetAll()
    {
        var instancies = new List<T1>();
        var prefabs =  base.GetAll();

        foreach (var prefab in prefabs)
            instancies.Add(Instantiate(prefab));

        return instancies;
    }

    public override IDictionary<string, T1> GetAllWithIDs()
    {
        var instancies = new Dictionary<string, T1>();
        var prefabHolders = base.GetAllWithIDs();

        foreach (var prefabHolder in prefabHolders)
            instancies.Add(prefabHolder.Key, Instantiate(prefabHolder.Value));

        return instancies;
    }
}