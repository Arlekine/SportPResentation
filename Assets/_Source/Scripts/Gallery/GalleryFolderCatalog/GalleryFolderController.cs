using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Gallery.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.GalleryFolderCatalog
{
    public class GalleryFolderController : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _content;
        [SerializeField] private FolderButton _buttonPrefab;

        [Space] 
        [SerializeField] private UIShowingAnimation _animation;
        
        private MediaSequenceCollectionSO _collection;
        private List<FolderButton> _buttons = new List<FolderButton>();
        
        public event Action<int> Selected;
        public event Action Back;

        [ProButton]
        public void Init(MediaSequenceCollectionSO collection)
        {
            _collection = collection;

            foreach (var collectionSequence in _collection.Sequences)
            {
                var button = Instantiate(_buttonPrefab, _content);
                button.Init(collectionSequence.TitlePhoto, collectionSequence.Title, _buttons.Count);
                _buttons.Add(button);
                button.Selected += OnSelected;
            }
        }

        public void Deactivate()
        {
            _animation.Hide();
        }

        [ProButton]
        public void Activate()
        {
            _animation.Show();
        }

        private void OnSelected(int obj)
        {
            Selected?.Invoke(obj);
        }

        private void OnBack()
        {
            Deactivate();
            Back?.Invoke();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBack);
        }
        
        private void OnDisable()
        {
            foreach (var folderButton in _buttons)
                folderButton.Selected -= OnSelected;
            
            _backButton.onClick.RemoveListener(OnBack);
        }
    }
}