// PlayerScoreController.cs
using UnityEngine;

public class PlayerScoreController
{
    public int CurrentScore { get; private set; }
    public int HighScore    { get; private set; }

    public PlayerScoreController()
    {
        ResetScore();
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        HUDManager.Instance.RaiseScoreChanged(0, HighScore);
    }

    // Puan ekle
    public void AddScore(int amount)
    {
        CurrentScore += amount;
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }
        
        HUDManager.Instance.RaiseScoreChanged(CurrentScore, HighScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        HUDManager.Instance.RaiseScoreChanged(0, HighScore);
    }
}
