using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }
    public event Action<int> OnTargetCountChanged;
    public event Action<int, int> OnScoreChanged;
    public event Action<int> OnHealthChanged;

    [Header("Text References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI targetCountText;

    [Header("Health References")]
    public Image[] hpIcons;


    [Header("Crosshair References")]
    public CanvasGroup crosshairCanvasGroup;
    public RectTransform crosshairRectTransform;

    private Vector3 hiddenScale = Vector3.zero;
    private Vector3 visibleScale = Vector3.one * 0.8f;
    private Transform currentTarget;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        crosshairCanvasGroup.alpha = 0f;
        crosshairRectTransform.localScale = hiddenScale;
    }

    
    private void Update()
    {
        if (currentTarget != null)
        {
            Vector2 screenPt = Camera.main.WorldToScreenPoint(currentTarget.position);
            RectTransform parentRT = crosshairRectTransform.parent as RectTransform;
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRT, screenPt, null, out anchoredPos);

            crosshairRectTransform
                .DOAnchorPos(anchoredPos, 0.1f)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);
        }
    }

    void Start()
    {
        SubscribeEvents();
    }

    void SubscribeEvents()
    {
        OnTargetCountChanged += UpdateTargetCountText;
        OnScoreChanged += UpdateScoreText;
        OnHealthChanged += HealthChanged;
    }

    void UnsubscribeEvents()
    {
        OnTargetCountChanged -= UpdateTargetCountText;
        OnScoreChanged -= UpdateScoreText;
        OnHealthChanged -= HealthChanged;
    }

    public void RaiseScoreChanged(int currentScore, int highScore)
        => OnScoreChanged?.Invoke(currentScore, highScore);

    public void RaiseTargetCountChanged(int count)
        => OnTargetCountChanged?.Invoke(count);

    public void RaiseHealthChanged(int currentHP)
        => OnHealthChanged?.Invoke(currentHP);

    void OnDisable()
    {
        UnsubscribeEvents();
    }
    public void UpdateScoreText(int score, int highScore)
    {
        scoreText.text = score.ToString() + "/" + highScore.ToString();
    }

    public void UpdateTargetCountText(int targetCount)
    {
        targetCountText.text = targetCount.ToString();
    }

    public void HealthChanged(int remainingHealth)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].gameObject.SetActive(i < remainingHealth);
        }
    }
    
    public void ShowCrosshair(Transform target)
    {
        currentTarget = target;

        // Mevcut tweenleri temizle
        crosshairCanvasGroup.DOKill();
        crosshairRectTransform.DOKill();

        // Fade-in + scale-in
        crosshairCanvasGroup.DOFade(1f, 0.15f);
        crosshairRectTransform
            .DOScale(visibleScale, 0.2f)
            .SetEase(Ease.OutBack);
    }

    public void HideCrosshair()
    {
        currentTarget = null;

        crosshairCanvasGroup.DOKill();
        crosshairRectTransform.DOKill();

        crosshairCanvasGroup
            .DOFade(0f, 0.1f)
            .OnComplete(() =>
            {
                crosshairRectTransform.localScale = hiddenScale;
            });
    }
}