using UnityEngine;
using UnityEngine.UI;

public static class GraphicExtensions
{
    public static void SetAlpha(this Graphic graphic, float alpha)
    {
        alpha = Mathf.Clamp01(alpha);
        var color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}