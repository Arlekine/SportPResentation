using Gallery.Data;
using Gallery.GalleryFileCatalog;
using Gallery.GalleryFolderCatalog;
using Gallery.PhotoLoop.App;
using UnityEngine;

namespace Gallery.App
{
    public class TwoGalleryScreensController : MonoBehaviour
    {
        [SerializeField] private MediaController _mediaController;
        [SerializeField] private GalleryFolderController _folderController;
        [SerializeField] private FileCatalogController _fileCatalogController;
        [SerializeField] private MediaSequenceCollectionSO _collection;

        private void Awake()
        {
            _folderController.Init(_collection);
            _fileCatalogController.Deactivate();
        }

        private void OnEnable()
        {
            _folderController.Selected += OnSelected;
            _fileCatalogController.Selected += OnFileToggled;
        }

        private void OnDisable()
        {
            _folderController.Selected -= OnSelected;
            _fileCatalogController.Selected -= OnFileToggled;
        }

        private void OnSelected(int index)
        {
            _folderController.Deactivate();
            _mediaController.ForceSwitchSequence(index);
            _fileCatalogController.Init(_collection.Sequences[index]);
        }

        private void OnFileToggled(FileButton button)
        {
            _mediaController.OnCatalogFileToggled(button);
        }
    }
}