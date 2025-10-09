using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ContentIDList<T1, T2> : ScriptableObject where T2 : IDHolder
{
    [Serializable]
    public class ContentHolder
    {
        [SerializeField] private T2 _idHolder;
        [SerializeField] private T1 _content;

        public string ID => _idHolder.ID;
        public T1 Content => _content;
    }

    [SerializeField] private List<ContentHolder> _contentHolders;

    public virtual T1 Get(string ID)
    {
        var holder = _contentHolders.Find(x => x.ID == ID);

        if (holder == null)
            throw new ArgumentException($"Factory {name} doesn't contain content with ID {ID}");
        
        return holder.Content;
    }

    public virtual IEnumerable<T1> GetAll()
    {
        return _contentHolders.Select(x => x.Content);
    }

    public virtual IDictionary<string, T1> GetAllWithIDs()
    {
        var dictionary = new Dictionary<string, T1>();
        _contentHolders.ForEach(x => dictionary.Add(x.ID, x.Content));
        return dictionary;
    }

    public bool ValidateIDs(IEnumerable<string> necessaryIDs, out IEnumerable<string> missingIDs)
    {
        var missing = new List<string>();

        foreach (var id in necessaryIDs)
        {
            if (_contentHolders.Exists(x => x.ID == id) == false)
                missing.Add(id);
        }

        missingIDs = missing;

        return !missingIDs.Any();
    }
}