using SquidGameVR.App;
using TMPro;
using UnityEngine;

public class PresentationScreenPlaceholder : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetText(string text) => _text.text = text;
}