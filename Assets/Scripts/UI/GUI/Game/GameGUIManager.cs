using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGUIManager : MonoBehaviour
{

    public static GameGUIManager Instance { get; set; }
    public BasePanel menuPanel;
    public BasePanel settingsPanel;
    public BasePanel gameEndPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        HideAllPanels();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPanel.IsOpen)
            {
                menuPanel.HidePanel();
            }
            else if (settingsPanel.IsOpen)
            {
                settingsPanel.HidePanel();
            }
            OpenMenu();
        }

        bool anyOpen = menuPanel.IsOpen || settingsPanel.IsOpen || gameEndPanel.IsOpen;
        Time.timeScale = anyOpen ? 0f : 1f;
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
        SceneManager.LoadScene("MainMenu");
    }

    public void HideAllPanels()
    {
        menuPanel.HidePanel();
        settingsPanel.HidePanel();
        gameEndPanel.HidePanel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
