using System;
using UnityEngine;

public static class GameEvents
{
    public static Action OnPlayerDeath;
    public static Action OnEnemyDestroyed;
    public static Action OnBombExplode;
    public static Action OnGameStart;
    public static Action OnGameOver;
    public static Action OnGamePaused;

    public static void BombExploded() => OnBombExplode?.Invoke();
    
    public static void EnemyKilled() => OnEnemyDestroyed?.Invoke();
    
    public static void PlayerKilled() => OnPlayerDeath?.Invoke();
    
    public static void GameStart() => OnGameStart?.Invoke();
    
    public static void GamePaused() => OnGamePaused?.Invoke();
    
    public static void GameOver() => OnGameOver?.Invoke();
}
