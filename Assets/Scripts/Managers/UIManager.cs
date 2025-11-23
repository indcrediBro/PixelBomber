using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TMP_Text[] scoresUI, bestScoresUI, livesUI, timerUI;

    private void OnEnable()
    {
        GameEvents.OnGamePaused += ShowPauseMenu;
        GameEvents.OnGameStart += UpdateScore;
        GameEvents.OnPlayerDeath += UpdateLivesUI;
    }

    private void OnDisable()
    {
        GameEvents.OnGamePaused -= ShowPauseMenu;
        GameEvents.OnGameStart -= UpdateScore;
        GameEvents.OnPlayerDeath -= UpdateLivesUI;
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        gameUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void ShowGameUI()
    {
        mainMenuUI.SetActive(false);
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);

    }

    public void ShowGameOver()
    {
        mainMenuUI.SetActive(false);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
        gameUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void UpdateScore()
    {
        if(scoresUI.Length == 0) return;
        
        UpdateCurrentScoresUI();
        UpdateBestScoresUI();
    }

    private void UpdateCurrentScoresUI()
    {
        foreach (TMP_Text scoreText in scoresUI)
        {
            if (scoreText == null)
            {
                Debug.LogError("scoresUI contains a null reference!");
                continue;
            }

            scoreText.text = ScoreManager.Instance.Score.ToString();
        }
    }


    private void UpdateBestScoresUI()
    {
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        int currentScore = ScoreManager.Instance.Score;

        if (currentScore > highscore)
        {
            highscore = currentScore; // Update local highscore
            PlayerPrefs.SetInt("HighScore", highscore);
        }

        foreach (TMP_Text scoreText in bestScoresUI)
        {
            scoreText.text = highscore.ToString();
        }
    }
    public void UpdateLivesUI()
    {
        Color c = GlobalData.Instance.lives <= 1 ? Color.red : Color.white;
        int l = GlobalData.Instance.lives;
        foreach (TMP_Text liveText in livesUI)
        {
            liveText.text = l.ToString();
        }
    }
    
    public void UpdateLevelTimeUI(float _time)
    {
        Color c = _time <= 30 ? Color.red : Color.white;
        foreach (TMP_Text timerText in timerUI)
        {
            timerText.color = c;
            timerText.text = _time.ToString("0:00");
        }
    }
}
