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
        [SerializeField] private GalleryFolderSelection _folderSelection;
        [SerializeField] private FileCatalogController _fileCatalogController;
        [SerializeField] private MediaSequenceCollectionSO _collection;

        private void Awake()
        {
            _folderSelection.Init(_collection);
            _fileCatalogController.Deactivate();
        }

        private void OnEnable() =>
            _folderSelection.Selected += OnSelected;
        
        private void OnDisable() =>
            _folderSelection.Selected -= OnSelected;
        
        private void OnSelected(int index)
        {
            _folderSelection.Deactivate();
            _mediaController.ForceSwitchSequence(index);
            _fileCatalogController.Init(_collection.Sequences[index]);
        }
    }
}