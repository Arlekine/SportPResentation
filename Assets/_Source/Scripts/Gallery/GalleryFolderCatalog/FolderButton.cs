using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.GalleryFolderCatalog
{
    public class FolderButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _titlePhoto;
        
        private int _folderIndex;
        
        public event Action<int> Selected; 

        public void Init(Sprite photo, string title, int index)
        {
            _titlePhoto.sprite = photo;
            _titleText.text = title;
            _folderIndex = index;
        }
        
        private void OnEnable()=>
            _button.onClick.AddListener(OnButtonClick);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClick);
        
        private void OnButtonClick() =>
            Selected?.Invoke(_folderIndex);
    }
}