using UnityEngine;

public class GameEndPanel : BasePanel
{
    public void OnExitToMenuClicked()
    {
        GUIManager.Instance.ReturnToMainMenu();
    }

    public void OnReplayClicked()
    {
        GUIManager.Instance.RestartGame();
    }
}
