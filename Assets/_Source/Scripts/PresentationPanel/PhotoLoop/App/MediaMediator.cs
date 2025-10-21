using PhotoLoop.View;
using PhotoLoop.Data;
using UnityEngine;

namespace PhotoLoop.App
{
    public sealed class MediaMediator
    {
        private readonly TripleMediaView _view;
        private readonly MediaSequenceCollectionSO _collection;

        private int _sequenceIndex;
        private int _photoIndex;
        private int _videoIndex;
        private bool _initialized;

        public MediaMediator(MediaSequenceCollectionSO collection, TripleMediaView view)
        {
            _collection = collection;
            _view = view;
        }

        public void SwitchFolder(int index)
        {
            if (_collection == null || _collection.Sequences.Count == 0)
                return;
            if (index < 0) 
                index = 0;
            
            if (index >= _collection.Sequences.Count) 
                index = _collection.Sequences.Count - 1;

            _sequenceIndex = index;
            _photoIndex = 0;
            _videoIndex = 0;
            _initialized = true;

            PrivateApplyFadeFromSequence();
            PrivateWarmupFirstFrame();
        }

        public void SwitchNextPhoto()
        {
            if (!_initialized) 
                return;
            
            var seq = _collection.Sequences[_sequenceIndex];
            if (seq.Photos.Count == 0) 
                return;
            

            if (seq.Videos.Count > 0)
            {
                var nextVideo = seq.Videos[PrivateWrap(_videoIndex, seq.Videos.Count)];
                _view.PrepareVideo(nextVideo);
            }

            var top = seq.Photos[PrivateWrap(_photoIndex + 0, seq.Photos.Count)];
            Texture2D mid = null;
            var bot = seq.Photos[PrivateWrap(_photoIndex + 2, seq.Photos.Count)];

            _view.PreparePhotos(top, mid, bot);
            _view.SwitchNextPhoto();
            _photoIndex += 3;
        }

        public void SwitchNextVideo()
        {
            if (!_initialized) 
                return;
            
            var seq = _collection.Sequences[_sequenceIndex];
            if (seq.Videos.Count == 0) 
                return;

            var next = seq.Videos[PrivateWrap(_videoIndex, seq.Videos.Count)];
            _view.PrepareVideo(next);
            _view.SwitchNextVideo();
            _videoIndex += 1;
        }

        public float GetPhotoDuration()
        {
            if (!_initialized) 
                return 3f;
            
            return _collection.Sequences[_sequenceIndex].PhotoDurationSeconds;
        }

        public float GetVideoDuration()
        {
            if (!_initialized)
                return 6f;
            
            return _collection.Sequences[_sequenceIndex].VideoDurationSeconds;
        }

        public float GetFadeDuration()
        {
            if (!_initialized) 
                return 0.5f;
            
            return _collection.Sequences[_sequenceIndex].FadeDurationSeconds;
        }

        public bool HasPhotos()
        {
            if (!_initialized) 
                return false;
            
            return _collection.Sequences[_sequenceIndex].Photos.Count > 0;
        }

        public bool HasVideos()
        {
            if (!_initialized)
                return false;
            
            return _collection.Sequences[_sequenceIndex].Videos.Count > 0;
        }

        private int PrivateWrap(int i, int count)
        {
            if (count <= 0) return 0;
            var m = i % count;
            if (m < 0) m += count;
            return m;
        }

        private void PrivateApplyFadeFromSequence()
        {
            var seq = _collection.Sequences[_sequenceIndex];
            
            _view.SetFadeDuration(seq.FadeDurationSeconds);
            _view.SetMiddleVideoOnly(seq.Videos.Count > 0);
        }

        private void PrivateWarmupFirstFrame()
        {
            var seq = _collection.Sequences[_sequenceIndex];

            Texture2D top = null;
            Texture2D bot = null;

            if (seq.Photos.Count > 0)
            {
                top = seq.Photos[PrivateWrap(0, seq.Photos.Count)];
                bot = seq.Photos[PrivateWrap(2, seq.Photos.Count)];
            }

            if (seq.Videos.Count > 0)
            {
                var firstVideo = seq.Videos[PrivateWrap(0, seq.Videos.Count)];
                _view.PrepareVideo(firstVideo);
                _view.PreparePhotos(top, null, bot);
                _view.SwitchNextPhoto();
                _view.SwitchNextVideo();
                _photoIndex = 3;
                _videoIndex = 1;
            }
            else
            {
                if (seq.Photos.Count > 0)
                {
                    var t = seq.Photos[PrivateWrap(0, seq.Photos.Count)];
                    var m = seq.Photos[PrivateWrap(1, seq.Photos.Count)];
                    var b = seq.Photos[PrivateWrap(2, seq.Photos.Count)];
                    _view.PreparePhotos(t, m, b);
                    _view.SwitchNextPhoto();
                    _photoIndex = 3;
                }
            }
        }
    }
}