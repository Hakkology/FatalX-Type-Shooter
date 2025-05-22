using UnityEngine;
using TMPro;

public class CreditsPanel : BasePanel
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI developerText;

    [Header("Content")]
    [SerializeField] private string developerName = "Your Name";

    protected override void Awake()
    {
        base.Awake();

        headerText.text     = "FATALIX SOFT\nWEB GAMES";
        developerText.text  = $"Developed by \n{developerName}";
    }
}
