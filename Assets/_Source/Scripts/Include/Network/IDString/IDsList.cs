using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ID_System/IDsList", fileName = "IDsList")]
public class IDsList : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<string> _ids = new List<string>() {"Test"};

    public IReadOnlyList<string> IDs => _ids;
    public string Name => _name;

    public bool Contains(string id) => _ids.Contains(id);

    public int GetIDIndex(string id)
    {
        if (Contains(id) == false)
            throw new ArgumentException($"Layer {nameof(id)} doesn't exist");

        return _ids.IndexOf(id);
    }

    public bool AddNewID(string id)
    {
        if (Contains(id))
            return false;

        _ids.Add(id);
        return true;
    }

    private void OnValidate()
    {
        _name = name;
        var distinctedList = _ids.Distinct().ToList();
        if (distinctedList.Count != _ids.Count)
            Debug.LogError("Duplicates found in list!");

        if (_ids.Count == 0)
            _ids.Add("Test");
    }
}