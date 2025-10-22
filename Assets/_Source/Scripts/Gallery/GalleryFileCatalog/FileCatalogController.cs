using System.Collections.Generic;
using DG.Tweening;
using Gallery.Data;
using UnityEngine;

namespace Gallery.GalleryFileCatalog
{
    public class FileCatalogController : MonoBehaviour
    {
        [SerializeField] private List<FileButton> _buttons;

        [Space] 
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.2f;
        
        private MediaSequenceSO _sequence;

        public void Init(MediaSequenceSO sequence)
        {
            _sequence = sequence;
            Activate();

            for (var i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].IsVideo)
                    return;
                
                _buttons[i].Init(i, _sequence.Photos[i]);
                _buttons[i].Selected += OnSelected;
            }
        }

        private void OnSelected(FileButton obj)
        {
            Debug.Log(obj.Index);
            
            foreach (var button in _buttons)
                if (button != obj)
                    button.Deactivate();
        }

        public void Activate()
        {
            _canvasGroup.DOFade(1f, _fadeDuration);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Deactivate()
        {
            _canvasGroup.DOFade(0f, _fadeDuration);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}