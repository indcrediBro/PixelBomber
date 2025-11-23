using System.Collections.Generic;
using UnityEngine;

public class GlobalData : Singleton<GlobalData>
{
    [SerializeField] private Sprite[] brickSprites;
    [SerializeField] private float playerMoveSpeed;
    
    [SerializeField] private GameObject exitPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bombExplosionPrefab;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject brickExplosionPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private int maxBombCount;
    [SerializeField] private float bombTimeToExplode;
    
    public GameObject activePlayer;
    public GameObject activeExit;
    public int maxLives = 3;
    public int lives;
    public float levelTimeLimit = 120f;
    public float levelTimeLeft;
    public float explosionLength = 2f;
    
    public List<Enemy> spawnedEnemies;

    private void OnEnable()
    {
        GameEvents.OnPlayerDeath += PlayerDied;
        GameEvents.OnGameStart += ResetLives;
        spawnedEnemies = new List<Enemy>();
    }

    private void PlayerDied()
    {
        lives--;
        if (lives == 0)
        {
            GameEvents.GameOver();
        }
    }
    
    private void OnDisable()
    {
        GameEvents.OnGameStart -= ResetLives;
        GameEvents.OnPlayerDeath -= PlayerDied;
        
    }

    private void ResetLives()
    {
        lives = maxLives;
        UIManager.Instance.UpdateLivesUI();
    }

    public void ResetLevelTime()
    {
        levelTimeLeft = levelTimeLimit;
        UIManager.Instance.UpdateLevelTimeUI(levelTimeLeft);
    }

    public GameObject GetExitPrefab()
    {
        return exitPrefab;
    }

    public Sprite GetRandomBrickSprite()
    {
        return brickSprites[Random.Range(0, brickSprites.Length)];
    }

    public GameObject GetBrickPrefab()
    {
        return brickPrefab;
    }

    public GameObject GetPlayerPrefab()
    {
        return playerPrefab;
    }
    
    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public float GetPlayerMoveSpeed()
    {
        return playerMoveSpeed;
    }

    public GameObject GetBombPrefab()
    {
        return bombPrefab;
    }

    public GameObject GetBombExplosionPrefab()
    {
        return bombExplosionPrefab;
    }
    
    public GameObject GetBrickExplosionPrefab()
    {
        return brickExplosionPrefab;
    }
    
    public int GetMaxBombCountAllowed()
    {
        return maxBombCount;
    }

    public float GetTimeToExplode()
    {
        return bombTimeToExplode;
    }

    public void IncreaseBombLimit()
    {
        maxBombCount++;
    }

    public void IncreaseCurrentHealthLimit()
    {
        lives++;
        UIManager.Instance.UpdateLivesUI();
    }

    public void IncreaseBombExplosionRangeLimit()
    {
        explosionLength += 0.5f;
    }
}
