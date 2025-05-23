using TMPro;
using UnityEngine;

public class StatBlock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI statValueText;

    public void Initialize(string statName, string statValue)
    {
        if (statNameText != null)
            statNameText.text = statName;
        if (statValueText != null)
            statValueText.text = statValue;
    }
}