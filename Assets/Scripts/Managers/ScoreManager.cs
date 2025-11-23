using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int Score { get; private set; }

    private void OnEnable()
    {
        GameEvents.OnGameStart += ResetScore;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= ResetScore;
    }

    public void AddScore(int _scoreToAdd)
    {
        Score += _scoreToAdd;
        UIManager.Instance.UpdateScore();
    }

    private void ResetScore()
    {
        Score = 0;
        UIManager.Instance.UpdateScore();
    }
}
