using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int Score { get; private set; }

    private void OnEnable()
    {
        GameEvents.OnEnemyDestroyed += IncreaseScore;
        GameEvents.OnGameStart += ResetScore;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDestroyed -= IncreaseScore;
        GameEvents.OnGameStart -= ResetScore;
    }

    public void IncreaseScore()
    {
        Score++;
        UIManager.Instance.UpdateScore();
    }

    private void ResetScore()
    {
        Score = 0;
        UIManager.Instance.UpdateScore();
    }
}
