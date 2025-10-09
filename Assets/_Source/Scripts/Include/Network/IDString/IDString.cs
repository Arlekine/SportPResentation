using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class IDAttribute : PropertyAttribute
{
    public string ListName { get; }

    public IDAttribute(string listName)
    {
        ListName = listName;
    }
}