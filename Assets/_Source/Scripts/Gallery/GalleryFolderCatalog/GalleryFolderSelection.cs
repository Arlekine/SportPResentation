using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using Gallery.Data;
using UnityEngine;

namespace Gallery.GalleryFolderCatalog
{
    public class GalleryFolderSelection : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private FolderButton _buttonPrefab;
        
        [Space]
        [SerializeField] private CanvasGroup _panelCanvasGroup;
        [SerializeField] private float _fadeDuration = 0.2f;
        
        private MediaSequenceCollectionSO _collection;
        private List<FolderButton> _buttons = new List<FolderButton>();
        
        public event Action<int> Selected;

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
            _panelCanvasGroup.DOFade(0f, _fadeDuration);
            _panelCanvasGroup.interactable = false;
            _panelCanvasGroup.blocksRaycasts = false;
        }

        public void Activate()
        {
            _panelCanvasGroup.DOFade(1f, _fadeDuration);
            _panelCanvasGroup.interactable = true;
            _panelCanvasGroup.blocksRaycasts = true;
        }

        private void OnSelected(int obj)
        {
            Selected?.Invoke(obj);
        }

        private void OnDisable()
        {
            foreach (var folderButton in _buttons)
                folderButton.Selected -= OnSelected;
        }
    }
}