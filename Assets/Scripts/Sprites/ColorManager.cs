using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance { get; private set; }

    public enum ShipColor
    {
        Red,    // 0
        Blue,   // 1
        Green,  // 2
        Yellow  // 3
    }

    private const string COLOR_PREF_KEY = "PlayerShipColor";
    public ShipColor CurrentColor { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadColor();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(ShipColor color)
    {
        CurrentColor = color;
        PlayerPrefs.SetInt(COLOR_PREF_KEY, (int)color);
        PlayerPrefs.Save();
        Debug.Log("Color Set: " + color);
    }

    private void LoadColor()
    {
        int savedColor = PlayerPrefs.GetInt(COLOR_PREF_KEY, (int)ShipColor.Red);
        CurrentColor = (ShipColor)savedColor;
        Debug.Log("Color Loaded: " + CurrentColor);
    }
}
