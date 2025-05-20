using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }
    public event Action<int> OnTargetCountChanged;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnHealthChanged;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI targetCountText;

    [Header("HP Icons (assign 3 icons here)")] 
    public Image[] hpIcons;  

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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

    public void RaiseScoreChanged(int delta)           => OnScoreChanged?.Invoke(delta);
    public void RaiseTargetCountChanged(int count)     => OnTargetCountChanged?.Invoke(count);
    public void RaiseHealthChanged(int currentHP)      => OnHealthChanged?.Invoke(currentHP);

    void OnDisable()
    {
        UnsubscribeEvents();
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
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
}