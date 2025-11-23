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
                Time.timeScale = 1;
                break;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && currentState == GameState.MainMenu)
        {
            StartGame();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (currentState == GameState.MainMenu)
            {
                // UIManager.Instance.ShowLeaderboard();
            }
            else if (currentState == GameState.Pause)
            {
                LevelManager.Instance.ToggleLevel();
                ChangeState(GameState.Game);
                UIManager.Instance.ShowGameUI();
            }
            else if (currentState == GameState.Game)
            {
                LevelManager.Instance.ToggleLevel();
                GameEvents.GamePaused();
                ChangeState(GameState.Pause);
            }
        }
    }

    public void StartGame()
    {
        LevelManager.Instance.LoadLevel();
        ChangeState(GameState.Game);
        GameEvents.GameStart();
    }

    public void GameOver()
    {
        LevelManager.Instance.ToggleLevel();
        ChangeState(GameState.GameOver);
    }
}
