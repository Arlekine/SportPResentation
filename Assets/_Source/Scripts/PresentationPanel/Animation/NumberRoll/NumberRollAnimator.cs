using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using TMPro;
using UnityEngine;

public sealed class NumberRollAnimator : UIShowingAnimation
{
    [SerializeField] private TMP_Text _label;
    
    [Space, Header("Settings")]
    [SerializeField] private int _from = 1;
    [SerializeField] private int _to = 60;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Ease _ease = Ease.Linear;
    [SerializeField] private bool _useUnscaledTime = false;
    [SerializeField] private bool _playOnEnable = false;
    [SerializeField] private string _format = "0";
    
    [Space, Header("Canvas group settings")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 0.3f;

    private Tween _tween;
    private int _current;

    public int CurrentValue => _current;

    private void OnEnable()
    {
        if (_playOnEnable) 
            Show();
    }

    private void OnDisable()
    {
        _tween?.Kill();
    }

    public override bool IsShowed { get; }

    [ProButton]
    public override Tween Show() =>
        Show(_from, _to, _duration);

    [ProButton]
    public override Tween Hide() =>
        _canvasGroup.DOFade(0f, _fadeDuration);

    public override void ShowInstantly() =>
        Show(_from, _to, 0f);

    public override void HideInstantly()=>
        _canvasGroup.DOFade(0f, _fadeDuration);

    public Tween Show(int from, int to, float duration)
    {
        _tween?.Kill();

        if (_canvasGroup.alpha == 0f)
            _canvasGroup.DOFade(1f, 0f);
        
        _current = from;
        UpdateLabel(_current);

        if (from == to || duration <= 0f)
        {
            _current = to;
            UpdateLabel(to);
            return null;
        }

        _tween = DOVirtual.Int(from, to, duration, v =>
        {
            if (v == _current) 
                return;
            _current = v;
            UpdateLabel(v);
        }).SetEase(_ease).SetUpdate(_useUnscaledTime).OnComplete(() =>
        {
            _current = to;
            UpdateLabel(to);
        });

        return _tween;
    }

    public void Stop(bool complete = false)
    {
        if (complete) 
            _tween?.Complete();
        
        _tween?.Kill();
    }

    public void SkipToEnd()
    {
        if (_tween != null && _tween.IsActive())
        {
            _tween.Complete();
            return;
        }
        _current = _to;
        UpdateLabel(_to);
    }

    public void SetTarget(int to, float duration)
    {
        Show(_current, to, duration);
    }

    private void UpdateLabel(int value)
    {
        if (_label == null) 
            return;
        _label.text = string.IsNullOrEmpty(_format) ? value.ToString() : value.ToString(_format);
    }
}