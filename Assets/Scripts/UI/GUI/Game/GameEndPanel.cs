using UnityEngine;

public class GameEndPanel : BasePanel
{
    public void OnExitToMenuClicked()
    {
        GameGUIManager.Instance.ReturnToMainMenu();
    }

    public void OnReplayClicked()
    {
        GameGUIManager.Instance.RestartGame();
    }
}
