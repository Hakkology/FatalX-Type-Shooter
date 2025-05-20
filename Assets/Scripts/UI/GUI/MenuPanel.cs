using UnityEngine;

public class MenuPanel : BasePanel
{
    public void OnContinueClicked()
    {
        ClosePanel();
    }

    public void OnSettingsClicked()
    {
        GUIManager.Instance.OpenSettings();
    }

    public void OnExitClicked()
    {
        GUIManager.Instance.ReturnToMainMenu();
    }
}
