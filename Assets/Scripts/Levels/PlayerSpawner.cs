using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private SpriteRenderer component;
    private GlobalData globalData;
    private BoxCollider2D componentCollider;
    
    private void Awake()
    {
        component = GetComponent<SpriteRenderer>();
        globalData = GlobalData.Instance;

        componentCollider = GetComponent<BoxCollider2D>();
        componentCollider.isTrigger = true;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.currentState != GameState.Game) return;

        if (globalData.activePlayer == null && globalData.lives > 0)
        {
            GameObject prefab = globalData.GetPlayerPrefab();
            globalData.activePlayer = Instantiate(prefab, transform.position, Quaternion.identity,
                FindFirstObjectByType<LevelGenerator>().activeLevel.transform);
            component.enabled = false;
        }

        if (globalData.activePlayer != null && globalData.lives > 0 && !globalData.activePlayer.activeInHierarchy)
        {
            globalData.activePlayer.transform.SetParent(FindFirstObjectByType<LevelGenerator>().activeLevel.transform);
            globalData.activePlayer.transform.position = transform.position;
            globalData.activePlayer.SetActive(true);
            component.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        if (GameManager.Instance.currentState != GameState.Game) return;
        if (globalData.activePlayer == null && globalData.lives > 0) component.enabled = true;
        if(globalData.activePlayer != null && globalData.lives > 0 && !globalData.activePlayer.activeInHierarchy)
            component.enabled = true;
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.currentState != GameState.Game) return;
        if (globalData.activePlayer == null && globalData.lives > 0) component.enabled = false;
        if (globalData.activePlayer != null && globalData.lives > 0 && !globalData.activePlayer.activeInHierarchy)
            component.enabled = false;
    }
}
