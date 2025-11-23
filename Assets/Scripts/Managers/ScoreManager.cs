using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private GameObject scorePrefab;
    
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
    
    public void AddScore(int _scoreToAdd, Vector3 _position)
    {
        GameObject scoreObject = Instantiate(scorePrefab, _position, Quaternion.identity);
        TMP_Text t = scoreObject.GetComponentInChildren<TMP_Text>();
        t.text = _scoreToAdd.ToString();
        scoreObject.SetActive(true);
        
        Score += _scoreToAdd;
        UIManager.Instance.UpdateScore();
    }

    private void ResetScore()
    {
        Score = 0;
        UIManager.Instance.UpdateScore();
    }
}
