using UnityEngine;

public static class RectTransformUtils
{
    public static Vector2 MatchPosition(RectTransform parent, RectTransform target)
    {
        Vector3 worldPos = target.position;
        Vector3 localPos = parent.InverseTransformPoint(worldPos);
        return localPos;
    }
}