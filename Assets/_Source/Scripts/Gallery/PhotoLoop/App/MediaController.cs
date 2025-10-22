using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gallery.Data;
using Gallery.GalleryFileCatalog;
using Gallery.PhotoLoop.View;
using UnityEngine;

namespace Gallery.PhotoLoop.App
{
    public sealed class MediaController : MonoBehaviour
    {
        [SerializeField] private MediaSequenceCollectionSO _collection;
        [SerializeField] private TripleMediaView _view;
        [SerializeField] private bool _useUnscaledTime;

        private MediaMediator _mediator;
        private CancellationTokenSource _cts;
        private MediaSequenceSO _currentSequence;
        private int _currentSequenceIndex = -1;
        private bool _catalogFocus;

        private void OnEnable()
        {
            _mediator = new MediaMediator(_collection, _view);
            PrivateStartLoop();
        }

        private void OnDisable()
        {
            PrivateStopLoop();
        }

        public void ForceSwitchSequence(int index)
        {
            _currentSequenceIndex = index;
            _mediator.SwitchFolder(index);
            _currentSequence = _collection != null && index >= 0 && index < _collection.Sequences.Count
                ? _collection.Sequences[index]
                : null;
            PrivateRestartLoop();
        }

        public void OnCatalogFileToggled(FileButton button)
        {
            if (_currentSequence == null || button == null)
                return;

            if (button.IsActive)
            {
                _catalogFocus = true;
                PrivateStopLoop();
                _view.HideTopAndBottom();
                _view.ClearMiddleVideos();

                if (button.IsVideo)
                {
                    if (button.Index >= 0 && button.Index < _currentSequence.Videos.Count)
                    {
                        _view.PrepareVideo(_currentSequence.Videos[button.Index]);
                        _view.SwitchNextVideo();
                    }
                }
                else
                {
                    if (button.Index >= 0 && button.Index < _currentSequence.Photos.Count)
                    {
                        var photo = _currentSequence.Photos[button.Index];
                        _view.ForceMiddlePhoto(photo);
                    }
                }
            }
            else
            {
                _catalogFocus = false;
                _view.ShowTopAndBottom();
                if (_currentSequenceIndex >= 0)
                    _mediator.SwitchFolder(_currentSequenceIndex);
                PrivateRestartLoop();
            }
        }

        private void PrivateRestartLoop()
        {
            if (_catalogFocus)
                return;

            PrivateStopLoop();
            PrivateStartLoop();
        }

        private void PrivateStartLoop()
        {
            _cts = new CancellationTokenSource();
            PrivateRunLoopAsync(_cts.Token).Forget();
        }

        private void PrivateStopLoop()
        {
            if (_cts == null)
                return;

            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private async UniTaskVoid PrivateRunLoopAsync(CancellationToken token)
        {
            var now = _useUnscaledTime ? Time.unscaledTime : Time.time;
            var photoWait = Mathf.Max(0f, _mediator.GetPhotoDuration());
            var videoWait = Mathf.Max(0f, _mediator.GetVideoDuration());
            var nextPhotoAt = _mediator.HasPhotos() ? now + photoWait : float.PositiveInfinity;
            var nextVideoAt = _mediator.HasVideos() ? now + videoWait : float.PositiveInfinity;

            while (!token.IsCancellationRequested)
            {
                now = _useUnscaledTime ? Time.unscaledTime : Time.time;
                var duePhoto = _mediator.HasPhotos() && now >= nextPhotoAt;
                var dueVideo = _mediator.HasVideos() && now >= nextVideoAt;

                if (duePhoto && dueVideo)
                {
                    _mediator.SwitchNextPhoto();
                    photoWait = Mathf.Max(0f, _mediator.GetPhotoDuration());
                    var stagger = Mathf.Max(0.001f, _mediator.GetFadeDuration());
                    nextPhotoAt = (_useUnscaledTime ? Time.unscaledTime : Time.time) + photoWait;
                    await UniTask.Delay(TimeSpan.FromSeconds(stagger), _useUnscaledTime, PlayerLoopTiming.Update, token);
                    _mediator.SwitchNextVideo();
                    videoWait = Mathf.Max(0f, _mediator.GetVideoDuration());
                    nextVideoAt = (_useUnscaledTime ? Time.unscaledTime : Time.time) + videoWait;
                }
                else if (duePhoto)
                {
                    _mediator.SwitchNextPhoto();
                    photoWait = Mathf.Max(0f, _mediator.GetPhotoDuration());
                    nextPhotoAt = (_useUnscaledTime ? Time.unscaledTime : Time.time) + photoWait;
                }
                else if (dueVideo)
                {
                    _mediator.SwitchNextVideo();
                    videoWait = Mathf.Max(0f, _mediator.GetVideoDuration());
                    nextVideoAt = (_useUnscaledTime ? Time.unscaledTime : Time.time) + videoWait;
                }
                else
                {
                    var nextDue = Mathf.Min(nextPhotoAt, nextVideoAt) - now;
                    if (float.IsInfinity(nextDue) || nextDue < 0f) nextDue = 0.05f;
                    var cap = Mathf.Clamp(nextDue, 0.01f, 0.25f);
                    await UniTask.Delay(TimeSpan.FromSeconds(cap), _useUnscaledTime, PlayerLoopTiming.Update, token);
                }
            }
        }
    }
}