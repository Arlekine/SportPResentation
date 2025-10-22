using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Cysharp.Threading.Tasks;
using Gallery.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Gallery.GalleryFileCatalog
{
    public class FileCatalogController : MonoBehaviour
    {
        [SerializeField] private List<FileButton> _buttons;
        [SerializeField] private Button _backButton;

        [Space, Header("Animation")] 
        [SerializeField] private UIShowingAnimation _animation;

        private MediaSequenceSO _sequence;
        private VideoPlayer _thumbnailPlayer;
        private readonly List<FileButton> _videoButtons = new List<FileButton>();
        private readonly List<FileButton> _photoButtons = new List<FileButton>();

        public event Action<FileButton> Selected;
        public event Action Back;

        public void Init(MediaSequenceSO sequence)
        {
            _sequence = sequence;
            Activate();

            _videoButtons.Clear();
            _photoButtons.Clear();

            if (_thumbnailPlayer == null)
            {
                _thumbnailPlayer = gameObject.AddComponent<VideoPlayer>();
                _thumbnailPlayer.renderMode = VideoRenderMode.RenderTexture;
                _thumbnailPlayer.playOnAwake = false;
                _thumbnailPlayer.isLooping = false;
                _thumbnailPlayer.audioOutputMode = VideoAudioOutputMode.None;
            }

            for (int i = 0; i < _buttons.Count; i++)
            {
                var button = _buttons[i];
                button.Selected -= OnSelected;
                
                if (button.IsVideo)
                    _videoButtons.Add(button);
                else
                    _photoButtons.Add(button);
            }

            for (int i = 0; i < _photoButtons.Count && i < _sequence.Photos.Count; i++)
            {
                var btn = _photoButtons[i];
                btn.Init(i, _sequence.Photos[i]);
                btn.Selected += OnSelected;
            }

            GenerateAllVideoPreviewsSequential().Forget();
        }
        
        private void OnEnable() =>
            _backButton.onClick.AddListener(OnBack);

        private void OnDisable() =>
            _backButton.onClick.RemoveListener(OnBack);

        private void OnBack()
        {
            Deactivate();
            Back?.Invoke();
        }

        private async UniTaskVoid GenerateAllVideoPreviewsSequential()
        {
            for (int i = 0; i < _videoButtons.Count && i < _sequence.Videos.Count; i++)
            {
                var btn = _videoButtons[i];
                btn.InitIndex(i);
                await GenerateVideoPreviewAsync(btn, _sequence.Videos[i]);
                btn.Selected += OnSelected;
            }
        }

        private void OnSelected(FileButton obj)
        {
            for (int i = 0; i < _buttons.Count; i++)
                if (_buttons[i] != obj)
                    _buttons[i].Deactivate();

            Selected?.Invoke(obj);
        }

        [ProButton]
        public void Activate()
        {
            _animation.Show();
        }

        public void Deactivate()
        {
            _animation.Hide();
        }

        private async UniTask GenerateVideoPreviewAsync(FileButton button, VideoClip clip)
        {
            if (clip == null)
                return;

            var clipWidth = clip.width > 0 ? (int)clip.width : 320;
            var clipHeight = clip.height > 0 ? (int)clip.height : 180;

            var rt = new RenderTexture(clipWidth, clipHeight, 0);
            _thumbnailPlayer.targetTexture = rt;
            _thumbnailPlayer.clip = clip;
            _thumbnailPlayer.Prepare();
            await UniTask.WaitUntil(() => _thumbnailPlayer.isPrepared);

            _thumbnailPlayer.time = 0;
            _thumbnailPlayer.Play();
            await UniTask.WaitUntil(() => _thumbnailPlayer.frame > 0);

            var prev = RenderTexture.active;
            RenderTexture.active = rt;

            var tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            tex.Apply();

            RenderTexture.active = prev;

            _thumbnailPlayer.Stop();
            _thumbnailPlayer.targetTexture = null;
            rt.Release();

            button.SetPreview(tex);
        }
    }
}