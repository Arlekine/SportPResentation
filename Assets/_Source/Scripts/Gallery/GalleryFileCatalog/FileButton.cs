using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.GalleryFileCatalog
{
    public class FileButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RawImage _image;
        [SerializeField] private bool _isVideo;
        
        [Space]
        [SerializeField] private CanvasGroup _panel;
        [SerializeField] private float _fadeDuration = 0.2f;
        
        private int _index;
        
        public event Action<FileButton> Selected;
        public bool IsVideo => _isVideo;
        public int Index => _index;

        public void Init(int index, Texture2D texture2D)
        {
            _index = index;
            _image.texture = texture2D;
            Deactivate();
        }
        
        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClick);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClick);
        
        private void OnButtonClick()
        {
            Selected?.Invoke(this);
            _panel.DOFade(1f, _fadeDuration);
            _panel.blocksRaycasts = true;
        }

        public void Deactivate()
        {
            _panel.DOFade(0f, _fadeDuration);
            _panel.blocksRaycasts = false;
        }
    }
}