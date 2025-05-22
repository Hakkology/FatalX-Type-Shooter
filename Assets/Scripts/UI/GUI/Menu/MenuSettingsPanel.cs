using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuSettingsPanel : SettingsBasePanel
{
    [Header("Color Toggles")]
    [SerializeField] private Toggle[] colorToggles;
    [SerializeField] private Image[] frameImages;  
    [SerializeField] private TextMeshProUGUI currentThemeText;

    [Space]
    [Header("Scale Highlight Settings")]
    [SerializeField] private float selectedScale = 1.1f;
    [SerializeField] private float scaleDuration = 0.3f;

    private int lastSelected = -1;

    protected override void SetupUI()
    {
        base.SetupUI();

        // hook up toggles
        for (int i = 0; i < colorToggles.Length; i++)
        {
            int idx = i;
            colorToggles[idx].onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                    OnColorToggleChanged((ThemeColor)idx);
            });
        }

        int currentIdx = (int)ColorManager.Instance.CurrentColor;
        if (currentIdx >= 0 && currentIdx < colorToggles.Length)
        {
            colorToggles[currentIdx].isOn = true;
            OnColorToggleChanged((ThemeColor)currentIdx);
        }
    }

    private void OnColorToggleChanged(ThemeColor color)
    {
        ColorManager.Instance.SetColor(color);
        currentThemeText.text = color.ToString();
        int idx = (int)color;

        if (lastSelected >= 0 && lastSelected < frameImages.Length)
        {
            var prev = frameImages[lastSelected];
            prev.transform.DOKill();
            prev.transform
                .DOScale(1f, scaleDuration)
                .SetEase(Ease.OutBack);
        }

        if (idx >= 0 && idx < frameImages.Length)
        {
            var now = frameImages[idx];
            now.transform.DOKill();
            now.transform
                .DOScale(selectedScale, scaleDuration)
                .SetEase(Ease.OutBack);
        }

        lastSelected = idx;
    }
}
