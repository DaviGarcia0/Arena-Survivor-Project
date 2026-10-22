using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UIManager ui;
    public WaveManager waves;
    public PlayerHealth player;
    public int Score { get; private set; }
    public bool IsFinished { get; private set; }

    private void Awake() { Instance = this; Time.timeScale = 1f; }
    private void Start()
    {
        ui.UpdateScore(0);
        ui.UpdateHealth(player.CurrentHealth, player.maxHealth);
        waves.Begin();
    }
    public void AddScore(int points)
    {
        if (IsFinished) return;
        Score += points;
        ui.UpdateScore(Score);
    }
    public void Finish(bool victory)
    {
        if (IsFinished) return;
        IsFinished = true;
        waves.StopAllCoroutines();
        ui.ShowResult(victory, Score);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
    private void OnDestroy() { if (Instance == this) Instance = null; Time.timeScale = 1f; }
}
