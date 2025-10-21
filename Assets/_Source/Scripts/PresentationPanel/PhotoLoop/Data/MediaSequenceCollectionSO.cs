using System.Collections.Generic;
using UnityEngine;

namespace PhotoLoop.Data
{
    [CreateAssetMenu(fileName = "MediaSequenceCollection", menuName = "Media/Sequence Collection")]
    public sealed class MediaSequenceCollectionSO : ScriptableObject
    {
        [SerializeField] private List<MediaSequenceSO> _sequences = new();
        public IReadOnlyList<MediaSequenceSO> Sequences => _sequences;
    }
}