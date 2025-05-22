using UnityEngine;

    public enum ThemeColor
    {
        Red,    // 0
        Blue,   // 1
        Green,  // 2
        Yellow  // 3
    }
    
public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance { get; private set; }



    private const string COLOR_PREF_KEY = "PlayerShipColor";
    public ThemeColor CurrentColor { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadColor();
            Debug.Log("ColorManager yüklendi: " + CurrentColor);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(ThemeColor color)
    {
        CurrentColor = color;
        PlayerPrefs.SetInt(COLOR_PREF_KEY, (int)color);
        PlayerPrefs.Save();
        Debug.Log("Color Set: " + color);
    }

    private void LoadColor()
    {
        int savedColor = PlayerPrefs.GetInt(COLOR_PREF_KEY, (int)ThemeColor.Red);
        CurrentColor = (ThemeColor)savedColor;
        Debug.Log("Color Loaded: " + CurrentColor);
    }
}
