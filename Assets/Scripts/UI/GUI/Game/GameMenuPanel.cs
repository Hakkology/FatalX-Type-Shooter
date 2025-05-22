using UnityEngine;

public class GameMenuPanel : BasePanel
{
    public void OnContinueClicked()
    {
        HidePanel();
    }

    public void OnSettingsClicked()
    {
        GameGUIManager.Instance.OpenSettings();
    }

    public void OnExitMenuClicked()
    {
        GameGUIManager.Instance.ReturnToMainMenu();
    }

    public void OnExitGameClicked()
    {
        Application.Quit();
    }
}
