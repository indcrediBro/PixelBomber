
using System;
using UnityEngine;

public class LevelManager: Singleton<LevelManager>
{
    [SerializeField] private LevelGenerator levelGenerator;
    private GlobalData globalData;

    private void Start()
    {
        globalData = GlobalData.Instance;
    }

    public void LoadLevel()
    {
        levelGenerator.GenerateLevel();
        globalData.ResetLevelTime();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentState != GameState.Game) return;

        if (globalData.activePlayer != null && globalData.activePlayer.activeInHierarchy && globalData.levelTimeLeft > 0)
        {
            globalData.levelTimeLeft -= Time.fixedDeltaTime;

            if (globalData.levelTimeLeft <= 0)
            {
                globalData.levelTimeLeft = 0;
                GameEvents.GameOver();
                ToggleLevel();
            }
            
            UIManager.Instance.UpdateLevelTimeUI(globalData.levelTimeLeft);
        }
    }

    public void ClearLevel()
    {
        LoadLevel();
    }

    public void ToggleLevel()
    {
        levelGenerator.ToggleLevelVisiblility();
    }
}