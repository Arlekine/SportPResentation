using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Gallery.PhotoLoop.View
{
    public sealed class TripleMediaView : MonoBehaviour
    {
        [Header("Top Slot")]
        [SerializeField] private CanvasGroup _topFrontGroup;
        [SerializeField] private CanvasGroup _topBackGroup;
        [SerializeField] private RawImage _topFrontImage;
        [SerializeField] private RawImage _topBackImage;

        [Space, Header("Middle Slot")]
        [SerializeField] private CanvasGroup _middleFrontGroup;
        [SerializeField] private CanvasGroup _middleBackGroup;
        [SerializeField] private RawImage _middleFrontImage;
        [SerializeField] private RawImage _middleBackImage;
        [SerializeField] private VideoPlayer _middleFrontVideo;
        [SerializeField] private VideoPlayer _middleBackVideo;
        [SerializeField] private RenderTexture _middleFrontRT;
        [SerializeField] private RenderTexture _middleBackRT;

        [Space, Header("Bottom Slot")]
        [SerializeField] private CanvasGroup _bottomFrontGroup;
        [SerializeField] private CanvasGroup _bottomBackGroup;
        [SerializeField] private RawImage _bottomFrontImage;
        [SerializeField] private RawImage _bottomBackImage;

        [Space, Header("Animation")]
        [SerializeField] private float _fadeDurationSeconds = 0.5f;

        [SerializeField] private bool _middleVideoOnly;

        private bool _topFrontActive = true;
        private bool _middleFrontActive = true;
        private bool _bottomFrontActive = true;

        public void SwitchNextPhoto()
        {
            PrivateCrossfadeTop();
            
            if (!_middleVideoOnly) 
                PrivateCrossfadeMiddle();
            
            PrivateCrossfadeBottom();
        }

        public void SwitchNextVideo()
        {
            PrivateCrossfadeMiddle();
        }

        internal void PreparePhotos(Texture2D topTexture, Texture2D middleTexture, Texture2D bottomTexture)
        {
            if (topTexture != null) 
                PrivateSetTopBackTexture(topTexture);
            
            if (!_middleVideoOnly && middleTexture != null)
            {
                PrivateStopMiddleBackVideo();
                PrivateSetMiddleBackPhoto(middleTexture);
            }

            if (bottomTexture != null)
                PrivateSetBottomBackTexture(bottomTexture);
        }

        internal void PrepareVideo(VideoClip clip)
        {
            PrivateSetMiddleBackVideo(clip);
        }

        internal void SetFadeDuration(float seconds)
        {
            _fadeDurationSeconds = Mathf.Max(0f, seconds);
        }

        internal void SetMiddleVideoOnly(bool value)
        {
            _middleVideoOnly = value;
        }

        private void PrivateCrossfadeTop()
        {
            var front = _topFrontActive ? _topFrontGroup : _topBackGroup;
            var back = _topFrontActive ? _topBackGroup : _topFrontGroup;
            
            DOTween.Kill(front);
            DOTween.Kill(back);
            
            back.alpha = 1f;
            front.DOFade(0f, _fadeDurationSeconds);
            _topFrontActive = !_topFrontActive;
            
            PrivateSwapInteractable(front, back);
        }

        private void PrivateCrossfadeMiddle()
        {
            var frontG = _middleFrontActive ? _middleFrontGroup : _middleBackGroup;
            var backG = _middleFrontActive ? _middleBackGroup : _middleFrontGroup;
            
            DOTween.Kill(frontG);
            DOTween.Kill(backG);
            
            backG.alpha = 1f;
            frontG.DOFade(0f, _fadeDurationSeconds);

            if (_middleFrontActive)
            {
                PrivateStopIfVideo(_middleFrontVideo);
                PrivatePlayIfVideo(_middleBackVideo);
            }
            else
            {
                PrivateStopIfVideo(_middleBackVideo);
                PrivatePlayIfVideo(_middleFrontVideo);
            }

            _middleFrontActive = !_middleFrontActive;
            PrivateSwapInteractable(frontG, backG);
        }

        private void PrivateCrossfadeBottom()
        {
            var front = _bottomFrontActive ? _bottomFrontGroup : _bottomBackGroup;
            var back = _bottomFrontActive ? _bottomBackGroup : _bottomFrontGroup;
            
            DOTween.Kill(front);
            DOTween.Kill(back);
            
            back.alpha = 1f;
            front.DOFade(0f, _fadeDurationSeconds);
            _bottomFrontActive = !_bottomFrontActive;
            
            PrivateSwapInteractable(front, back);
        }

        private void PrivateSwapInteractable(CanvasGroup front, CanvasGroup back)
        {
            front.blocksRaycasts = false;
            back.blocksRaycasts = true;
            
            front.interactable = false;
            back.interactable = true;
        }

        private void PrivateSetTopBackTexture(Texture2D tex)
        {
            if (_topFrontActive) 
                _topBackImage.texture = tex;
            else 
                _topFrontImage.texture = tex;
        }

        private void PrivateSetBottomBackTexture(Texture2D tex)
        {
            if (_bottomFrontActive) 
                _bottomBackImage.texture = tex;
            else 
                _bottomFrontImage.texture = tex;
        }

        private void PrivateSetMiddleBackPhoto(Texture2D tex)
        {
            if (_middleFrontActive)
            {
                _middleBackVideo.targetTexture = null;
                _middleBackVideo.clip = null;
                _middleBackImage.texture = tex;
            }
            else
            {
                _middleFrontVideo.targetTexture = null;
                _middleFrontVideo.clip = null;
                _middleFrontImage.texture = tex;
            }
        }

        private void PrivateSetMiddleBackVideo(VideoClip clip)
        {
            if (_middleFrontActive)
            {
                _middleBackImage.texture = _middleBackRT;
                _middleBackVideo.clip = clip;
                _middleBackVideo.targetTexture = _middleBackRT;
                _middleBackVideo.isLooping = false;
                _middleBackVideo.playOnAwake = false;
                
                _middleBackVideo.Prepare();
            }
            else
            {
                _middleFrontImage.texture = _middleFrontRT;
                _middleFrontVideo.clip = clip;
                _middleFrontVideo.targetTexture = _middleFrontRT;
                _middleFrontVideo.isLooping = false;
                _middleFrontVideo.playOnAwake = false;
                
                _middleFrontVideo.Prepare();
            }
        }

        private void PrivatePlayIfVideo(VideoPlayer vp)
        {
            if (vp != null && vp.clip != null) 
                vp.Play();
        }

        private void PrivateStopIfVideo(VideoPlayer vp)
        {
            if (vp != null && vp.isPlaying) 
                vp.Stop();
        }

        private void PrivateStopMiddleBackVideo()
        {
            if (_middleFrontActive)
            {
                if (_middleBackVideo != null && _middleBackVideo.isPlaying) 
                    _middleBackVideo.Stop();
                
                _middleBackVideo.clip = null;
                _middleBackVideo.targetTexture = null;
            }
            else
            {
                if (_middleFrontVideo != null && _middleFrontVideo.isPlaying) 
                    _middleFrontVideo.Stop();
                
                _middleFrontVideo.clip = null;
                _middleFrontVideo.targetTexture = null;
            }
        }
    }
}