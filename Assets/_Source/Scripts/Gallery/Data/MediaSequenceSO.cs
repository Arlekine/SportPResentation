using System.Collections.Generic;
using UnityEngine;

namespace Gallery.Data
{
    [CreateAssetMenu(fileName = "MediaSequence", menuName = "Media/Sequence")]
    public class MediaSequenceSO : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _titlePhoto;
        [SerializeField] private List<Texture2D> _photos = new();
        [SerializeField] private List<UnityEngine.Video.VideoClip> _videos = new();
        [SerializeField] private float _photoDurationSeconds = 3f;
        [SerializeField] private float _videoDurationSeconds = 6f;
        [SerializeField] private float _fadeDurationSeconds = 0.5f;

        public string Title => _title;
        public Sprite TitlePhoto => _titlePhoto;
        public IReadOnlyList<Texture2D> Photos => _photos;
        public IReadOnlyList<UnityEngine.Video.VideoClip> Videos => _videos;
        public float PhotoDurationSeconds => _photoDurationSeconds;
        public float VideoDurationSeconds => _videoDurationSeconds;
        public float FadeDurationSeconds => _fadeDurationSeconds;
    }
}