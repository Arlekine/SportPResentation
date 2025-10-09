using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class FixedAnchoredYAppearAnimation : AnchoredMoveAppearAnimation
{
    public override Vector2 ShowPosition => new Vector2(RectTransform.anchoredPosition.x, _showPosition);
    public override Vector2 HidePosition => new Vector2(RectTransform.anchoredPosition.x, _hidePosition);

    [Space]
    [SerializeField] private float _showPosition;
    [SerializeField] private float _hidePosition;

    private RectTransform RectTransform
    {
        get
        {
            if (_panel == null)
                _panel = GetComponent<RectTransform>();

            return _panel;
        }
    }

    [ProButton]
    private void SetOffsetAnimation(float offset)
    {
        _showPosition = _panel.anchoredPosition.y;
        _hidePosition = _panel.anchoredPosition.y + offset;
    }
}