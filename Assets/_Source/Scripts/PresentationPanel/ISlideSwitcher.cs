using System;
using UnityEngine;

public interface ISlideSwitcher
{
    event Action OpenScreenSaver;
    event Action<string, string> SwitchChapter;
    event Action<int> SwitchSubchapter;
    event Action CloseSubchapter;
}