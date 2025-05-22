using UnityEngine;

public class GameMenuPanel : BasePanel
{
    public void OnContinueClicked()
    {
        ClosePanel();
    }

    public void OnSettingsClicked()
    {
        GameGUIManager.Instance.OpenSettings();
    }

    public void OnExitClicked()
    {
        GameGUIManager.Instance.ReturnToMainMenu();
    }
}
