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

    // Unity oyununuzda exit fonksiyonu için
    public void OnExitPressed()
    {
        // WebGL için özel bir kontrol
        #if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalEval("if(typeof exitUnityGame === 'function') exitUnityGame();");
        #else
            Application.Quit();
        #endif
    }
}
