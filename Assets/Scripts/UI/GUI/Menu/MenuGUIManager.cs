using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private BasePanel creditsPanel;   
    [SerializeField] private BasePanel settingsPanel;    

    private void Start()
    {
        creditsPanel.ClosePanel();
        settingsPanel.ClosePanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.IsOpen)
                settingsPanel.ClosePanel();
            if (creditsPanel.IsOpen)
                creditsPanel.ClosePanel();
        }
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("ShooterScene");
    }

    public void OnCreditsPressed()
    {
        if (creditsPanel.IsOpen)
            settingsPanel.ClosePanel();

        settingsPanel.ClosePanel();
        creditsPanel.OpenPanel();
    }

    public void OnSettingsPressed()
    {
        if (settingsPanel.IsOpen)
            settingsPanel.ClosePanel();
        
        creditsPanel.ClosePanel();
        settingsPanel.OpenPanel();
    }

    public void OnExitPressed()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
