using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private MenuBasePanel mainMenuPanel;
    [SerializeField] private BasePanel settingsPanel;
    [SerializeField] private BasePanel creditsPanel;

    private void Start()
    {
        settingsPanel.ClosePanel();
        creditsPanel.ClosePanel();
        mainMenuPanel.OpenPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.IsOpen)
            {
                settingsPanel.ClosePanel();
                mainMenuPanel.OpenPanel();
            }
            else if (creditsPanel.IsOpen)
            {
                creditsPanel.ClosePanel();
                mainMenuPanel.OpenPanel();
            }
            else if (mainMenuPanel.IsOpen)
            {
                OnExitPressed();
            }
        }
    }

    public void OnPlayPressed()
    {
        mainMenuPanel.ClosePanel();
        SceneManager.LoadScene("ShooterScene");
    }

    public void OnCreditsPressed()
    {
        mainMenuPanel.ClosePanel();
        settingsPanel.ClosePanel();
        creditsPanel.OpenPanel();
    }

    public void OnSettingsPressed()
    {
        mainMenuPanel.ClosePanel();
        creditsPanel.ClosePanel();
        settingsPanel.OpenPanel();
    }

    public void OnBackToMenuPressed()
    {
        settingsPanel.ClosePanel();
        creditsPanel.ClosePanel();
        mainMenuPanel.OpenPanel();
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
