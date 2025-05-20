using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    [SerializeField] private MenuPanel menuPanel;
    [SerializeField] private SettingsPanel settingsPanel;
    [SerializeField] private GameEndPanel gameEndPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ToggleMenu()
    {
        if (menuPanel.gameObject.activeSelf)
            menuPanel.ClosePanel();
        else
            menuPanel.OpenPanel();
    }

    public void OpenSettings()
    {
        menuPanel.HidePanel();
        settingsPanel.OpenPanel();
    }

    public void OpenMenu()
    {
        settingsPanel.HidePanel();
        menuPanel.OpenPanel();
    }

    public void ShowGameEnd()
    {
        gameEndPanel.OpenPanel();
    }

    public void ReturnToMainMenu()
    {
        // Örneğin sahne yükleme: SceneManager.LoadScene("MainMenu");
        Debug.Log("Returning to main menu...");
    }

    public void RestartGame()
    {
        // Örneğin sahne reload: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restarting game...");
    }
}
