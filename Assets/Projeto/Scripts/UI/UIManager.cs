using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthFill;
    public TMP_Text scoreText;
    public TMP_Text waveText;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public TMP_Text gameOverScore;
    public TMP_Text victoryScore;

    public void UpdateHealth(int health, int maximum) { healthFill.fillAmount = (float)health / maximum; }
    public void UpdateScore(int score) { scoreText.text = "PONTOS: " + score; }
    public void UpdateWave(int wave) { waveText.text = "ONDA " + wave + " / 3"; }
    public void ShowResult(bool victory, int score)
    {
        gameOverPanel.SetActive(!victory);
        victoryPanel.SetActive(victory);
        (victory ? victoryScore : gameOverScore).text = "PONTUAÇÃO FINAL\n" + score;
    }
}
