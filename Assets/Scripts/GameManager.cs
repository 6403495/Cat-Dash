

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static bool startWithCountdown = false;
    public bool IsGameOver { get; private set; }
    public bool IsPlaying { get; private set; }

    [Header("UI")]
    public GameOverUI gameOverUI;

    [Header("Score")]
    public ScoreManager scoreManager;

    private bool hasFinished;

    void Awake()
    {
        Instance = this;

        Time.timeScale = 0f;

        IsGameOver = false;
        IsPlaying = false;
        hasFinished = false;
    }

    void Start()
    {
        if (startWithCountdown)
        {
            startWithCountdown = false;
            StartGame();
        }
        else
        {
            if (gameOverUI != null)
                gameOverUI.ShowMainMenu();
        }
    }

    public void StartGame()
    {
        if (IsPlaying || IsGameOver || hasFinished)
            return;

        if (scoreManager != null)
            scoreManager.ResetScore();

        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        IsPlaying = false;
        Time.timeScale = 0f;

        gameOverUI.ShowCountdown();

        string[] countdown = { "3", "2", "1", "GO!" };

        for (int i = 0; i < countdown.Length; i++)
        {
            gameOverUI.SetCountdownText(countdown[i]);

            yield return new WaitForSecondsRealtime(
                i == 3 ? 0.5f : 1f
            );
        }

        gameOverUI.ShowGameplay();

        IsPlaying = true;
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        if (IsGameOver || hasFinished)
            return;

        IsGameOver = true;
        IsPlaying = false;

        Time.timeScale = 0f;

        int finalScore = 0;

        if (scoreManager != null)
            finalScore = scoreManager.score;

        gameOverUI.ShowGameOver(finalScore);

        Debug.Log("GAME OVER!");
    }

    public void FinishGame()
    {
        if (IsGameOver || hasFinished)
            return;

        hasFinished = true;
        IsPlaying = false;

        Time.timeScale = 0f;

        int finalScore = 0;

        if (scoreManager != null)
            finalScore = scoreManager.score;

        gameOverUI.ShowFinish(finalScore);

        Debug.Log("YOU WIN!");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            Time.timeScale = 1f;
        }
    }

    public void RetryGame()
    {
        Debug.Log("RETRY GAME STARTED");

        IsGameOver = false;
        IsPlaying = false;
        hasFinished = false;

        Time.timeScale = 1f;

        if (scoreManager != null)
            scoreManager.ResetScore();

        StartCoroutine(CountdownRoutine());
    }

    public void RetryReload()
    {
        startWithCountdown = true;
        ReloadScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            FinishGame();
        }
    }
}



