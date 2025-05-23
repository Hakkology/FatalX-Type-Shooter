using UnityEngine;

public class GameEndPanel : BasePanel
{
    public GameObject statBlockPrefab;
    public void OnExitToMenuClicked()
    {
        GameGUIManager.Instance.ReturnToMainMenu();
    }

    public void OnReplayClicked()
    {
        GameGUIManager.Instance.RestartGame();
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        PopulateStats();
    }

    private void PopulateStats()
    {
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);

        var stats = new (string name, string value)[]
        {
            ("Destroyed Objects", GameStatsController.Instance.DestroyedObjectCount.ToString()),
            ("Asteroids Hit",    GameStatsController.Instance.HitAsteroidCount.ToString()),
            ("Ships Hit",        GameStatsController.Instance.HitShipCount.ToString()),
            ("Correct Keys",     GameStatsController.Instance.CorrectKeyPressCount.ToString()),
            ("Wrong Keys",       GameStatsController.Instance.WrongKeyPressCount.ToString()),
            ("Accuracy",         GameStatsController.Instance.Accuracy.ToString("F1") + "%")
        };

        foreach (var (name, value) in stats)
        {
            var blockGO = Instantiate(statBlockPrefab, this.transform);
            var block = blockGO.GetComponent<StatBlock>();
            if (block != null)
                block.Initialize(name, value);
            else
                Debug.LogWarning("StatBlock component missing on prefab.");
        }
    }
}
