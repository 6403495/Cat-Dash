

using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject countdownPanel;
    public GameObject gameplayPanel;
    public GameObject gameOverPanel;
    public GameObject finishPanel;

    [Header("Countdown")]
    public TMP_Text countdownText;

    [Header("Final Scores")]
    public TMP_Text gameOverScoreText;
    public TMP_Text finishScoreText;

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        countdownPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    public void ShowCountdown()
    {
        mainMenuPanel.SetActive(false);
        countdownPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    public void SetCountdownText(string value)
    {
        countdownText.text = value;
    }

    public void ShowGameplay()
    {
        mainMenuPanel.SetActive(false);
        countdownPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    public void ShowGameOver(int score)
    {
        mainMenuPanel.SetActive(false);
        countdownPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        finishPanel.SetActive(false);

        gameOverScoreText.text = "FINAL SCORE: " + score;
    }

    public void ShowFinish(int score)
    {
        mainMenuPanel.SetActive(false);
        countdownPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        finishPanel.SetActive(true);

        finishScoreText.text = "FINAL SCORE: " + score;
    }

    public void RestartGame()
    {
        GameManager.Instance.RetryReload();
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.ReloadScene();
    }
}



