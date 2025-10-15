using System;
using Unity.Netcode;

public class NetworkSlideSwitcher : NetworkBehaviour, ISlideSwitcher
{
    public event Action OpenScreenSaver;
    public event Action<string, string> SwitchChapter;
    public event Action<int> SwitchSubchapter;
    public event Action CloseSubchapter;

    [ServerRpc(RequireOwnership = false)]
    public void OpenScreenSaverServerRpc() => OpenScreenSaver?.Invoke();

    [ServerRpc(RequireOwnership = false)]
    public void SwitchChapterServerRpc(string regionID, string chapterID) => SwitchChapter?.Invoke(regionID, chapterID);

    [ServerRpc(RequireOwnership = false)]
    public void SwitchSubchapterServerRpc(int index) => SwitchSubchapter?.Invoke(index);

    [ServerRpc(RequireOwnership = false)]
    public void CloseSubchapterServerRpc() => CloseSubchapter?.Invoke();
}