using System;
using UnityEngine;

public class RegionButton : IDButton
{
    private const string NAME_FORMAT = "Region_Button_{0}";

    [SerializeField] [ID("Regions")] private string _regionID;
    public override string ID => _regionID;
    private string ObjectName => String.Format(NAME_FORMAT, ID);

    public override void Select() {}
    public override void Deselect(){}

    private void OnValidate()
    {
        if (gameObject.name != ObjectName)
            gameObject.name = ObjectName;
    }
}