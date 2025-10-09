using UnityEngine;

public class RegionButton : IDButton
{
    [SerializeField] [ID("Regions")] private string _regionID;
    public override string ID => _regionID;
}