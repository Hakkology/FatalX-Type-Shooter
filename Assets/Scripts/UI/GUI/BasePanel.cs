using UnityEngine;
using DG.Tweening;

public abstract class BasePanel : MonoBehaviour, IBasePanel
{
    [SerializeField] protected CanvasGroup canvasGroup;
    protected Tween currentTween;

    [SerializeField] protected float transitionDuration = 0.25f;
    [SerializeField] protected Ease transitionEase = Ease.OutCubic;

    protected virtual void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OpenPanel()
    {
        currentTween?.Kill();
        gameObject.SetActive(true);
        currentTween = canvasGroup.DOFade(1f, transitionDuration)
            .SetEase(transitionEase)
            .OnStart(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    public virtual void ClosePanel()
    {
        currentTween?.Kill();
        currentTween = canvasGroup.DOFade(0f, transitionDuration)
            .SetEase(transitionEase)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
            });
    }

    public virtual void HidePanel() // sadece görünmez yapar ama aktif bırakır
    {
        currentTween?.Kill();
        currentTween = canvasGroup.DOFade(0f, transitionDuration)
            .SetEase(transitionEase)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }
}
