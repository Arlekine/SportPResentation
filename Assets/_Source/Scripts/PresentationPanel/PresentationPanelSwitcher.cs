using System;
using Cysharp.Threading.Tasks;
using SportPresentation.App;
using SquidGameVR.App;
using UnityEngine;
using Object = UnityEngine.Object;

public class PresentationPanelSwitcher : IDisposable
{
    private const string PLACEHOLDER_PATH = "PresentationPlaceHolderScreen";

    private ContentLoader _contentLoader;
    private ISlideSwitcher _slideSwitcher;
    
    private PresentationScreenPlaceholder _presentationScreenPlaceholder;

    private string _currentRegion;
    private string _currentChapter;
    private int _lastIndex;

    public async UniTask Init()
    {
        _contentLoader = ServiceLocator.Instance.GetService<ContentLoader>();
        _slideSwitcher = ServiceLocator.Instance.GetService<ISlideSwitcher>();

        ServiceLocator.Instance.GetService<IDisposablesHolder>().Add(this);

        _slideSwitcher.OpenScreenSaver += OpenScreenSaver;
        _slideSwitcher.SwitchChapter += OpenScreen;
        _slideSwitcher.SwitchSubchapter += OpenSubchapter;
        _slideSwitcher.CloseSubchapter += CloseCurrentSubchapter;
        
        var presentationPlaceHolder = await _contentLoader.LoadPresentationPanelContent<PresentationScreenPlaceholder>(PLACEHOLDER_PATH);
        _presentationScreenPlaceholder = Object.Instantiate(presentationPlaceHolder);
    }

    public void OpenScreenSaver()
    {
        _presentationScreenPlaceholder.SetText("SCREENSAVER");
    }

    public void OpenScreen(string regionID, string chapterID)
    {
        _currentRegion = regionID;
        _currentChapter = chapterID;

        UpdateText();
    }

    public void OpenSubchapter(int index)
    {
        _lastIndex = index;
        
        UpdateText();
    }

    public void CloseCurrentSubchapter()
    {
        _lastIndex = -1;

        UpdateText();
    }


    public void Dispose()
    {
        _slideSwitcher.OpenScreenSaver -= OpenScreenSaver;
        _slideSwitcher.SwitchChapter -= OpenScreen;
        _slideSwitcher.SwitchSubchapter -= OpenSubchapter;
        _slideSwitcher.CloseSubchapter -= CloseCurrentSubchapter;
    }

    private void UpdateText()
    {
        var text = $"{_currentRegion} - {_currentChapter}";

        if (_lastIndex != -1)
            text += $" - {_lastIndex}";

        _presentationScreenPlaceholder.SetText(text);
    }
}
