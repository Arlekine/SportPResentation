using UnityEngine;

public class RectTransformConstrainer : MonoBehaviour
{
    [SerializeField] private RectTransform _matchTo;
    [SerializeField] private RectTransform _matchToParent;
    [SerializeField] private RectTransform _matchFrom;

    private void LateUpdate()
    {
        _matchTo.anchoredPosition = RectTransformUtils.MatchPosition(_matchToParent, _matchFrom);
    }
}