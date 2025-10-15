using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public static class DOTweenExtensions
{
    public static TweenerCore<float, float, FloatOptions> DOFontSize(this TMP_Text text, float targetSize, float duration)
    {
        var tween = DOTween.To(() => text.fontSize, size => text.fontSize = size, targetSize, duration);
        tween.SetTarget(text);
        return tween;
    }

    public static TweenerCore<float, float, FloatOptions> DOPrefferedHeight(this LayoutElement layoutElement, float targetHeight, float duration)
    {
        var tween = DOTween.To(() => layoutElement.preferredHeight, height => layoutElement.preferredHeight = height, targetHeight, duration);
        tween.SetTarget(layoutElement);
        return tween;
    }
}
