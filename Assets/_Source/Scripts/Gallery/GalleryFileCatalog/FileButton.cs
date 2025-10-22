using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.GalleryFileCatalog
{
    public class FileButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RawImage _image;
        [SerializeField] private bool _isVideo;
        
        [Space, Header("Animation")] 
        [SerializeField] private UIShowingAnimation _animation;

        private bool _isActivate;
        private int _index;

        public event Action<FileButton> Selected;
        public bool IsVideo => _isVideo;
        public int Index => _index;
        public bool IsActive => _isActivate;

        public void Init(int index, Texture2D texture2D)
        {
            _index = index;
            _image.texture = texture2D;
        }

        public void InitIndex(int index)
        {
            _index = index;
        }

        public void SetPreview(Texture texture)
        {
            _image.texture = texture;
        }

        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick()
        {
            if (IsActive)
                Deactivate();
            else
                Activate();

            Selected?.Invoke(this);
        }

        public void Activate()
        {
            _animation.Show();
            _isActivate = true;
        }

        public void Deactivate()
        {
            _animation.Hide();
            _isActivate = false;
        }
    }
}