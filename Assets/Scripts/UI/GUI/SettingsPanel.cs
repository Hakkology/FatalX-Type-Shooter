using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsPanel : SettingsBasePanel
{
    [Header("Ship Color Selection")]
    [SerializeField] private Toggle redToggle;
    [SerializeField] private Toggle blueToggle;
    [SerializeField] private Toggle greenToggle;
    [SerializeField] private Toggle yellowToggle;

    [SerializeField] private float animationScale = 1.15f;
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private Ease animationEase = Ease.OutBack;

    protected override void Awake()
    {
        base.Awake();

        redToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) SelectColor(ColorManager.ShipColor.Red, redToggle);
        });

        blueToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) SelectColor(ColorManager.ShipColor.Blue, blueToggle);
        });

        greenToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) SelectColor(ColorManager.ShipColor.Green, greenToggle);
        });

        yellowToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) SelectColor(ColorManager.ShipColor.Yellow, yellowToggle);
        });
    }

    private void OnEnable()
    {
        var current = ColorManager.Instance.CurrentColor;

        redToggle.isOn = current == ColorManager.ShipColor.Red;
        blueToggle.isOn = current == ColorManager.ShipColor.Blue;
        greenToggle.isOn = current == ColorManager.ShipColor.Green;
        yellowToggle.isOn = current == ColorManager.ShipColor.Yellow;
    }

    private void SelectColor(ColorManager.ShipColor color, Toggle toggle)
    {
        ColorManager.Instance.SetColor(color);
        AnimateToggle(toggle);
    }

    private void AnimateToggle(Toggle toggle)
    {
        // Scale'i sıfırlayıp tekrar başlatmak için önce kill et
        toggle.transform.DOKill();
        toggle.transform.localScale = Vector3.one;

        toggle.transform
            .DOScale(animationScale, animationDuration)
            .SetEase(animationEase)
            .OnComplete(() =>
            {
                toggle.transform.DOScale(1f, animationDuration / 2f).SetEase(Ease.InOutQuad);
            });
    }

    public void OnBackClicked()
    {
        GUIManager.Instance.OpenMenu();
    }
}
