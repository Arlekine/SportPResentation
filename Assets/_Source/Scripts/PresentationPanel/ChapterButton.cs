using UnityEngine;

public class ChapterButton : IDButton
{
    [SerializeField][ID("Chapters")] private string _chapterID;
    public override string ID => _chapterID;
}