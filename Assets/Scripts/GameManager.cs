using UnityEngine;
using System;

public enum GameState { MainMenu, Game , Pause, GameOver }

public class GameManager : Singleton<GameManager>
{
    public GameState currentState;
    [SerializeField] private LevelGenerator levelGenerator;
    
    private void OnEnable()
    {
        GameEvents.OnGameOver += GameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= GameOver;
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.MainMenu:
                UIManager.Instance.ShowMainMenu();
                Time.timeScale = 1f;
                break;
            case GameState.Pause:
                UIManager.Instance.ShowPauseMenu();
                Time.timeScale = 0; // Pause the game
                break;
            case GameState.Game:
                UIManager.Instance.ShowGameUI();
                Time.timeScale = 1; // Resume the game
                break;
            case GameState.GameOver:
                UIManager.Instance.ShowGameOver();
                Time.timeScale = 0;
                break;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && currentState == GameState.MainMenu)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Game);
        levelGenerator.GenerateLevel();
        GameEvents.GameStart();
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }
}
