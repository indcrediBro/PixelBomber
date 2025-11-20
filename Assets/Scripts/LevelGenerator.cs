using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator: MonoBehaviour
{
    [SerializeField] GameObject[] levelPrefabs;
    [SerializeField] private float brickSpawnRate;
    [SerializeField] private float enemySpawnRate;
    private GameObject activeLevel;
    
    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        LevelData currentLevel = GetRandomLevel();
        activeLevel = Instantiate(currentLevel, Vector3.zero, Quaternion.identity).gameObject;
        Sprite brickSprite = GlobalData.Instance.GetRandomBrickSprite();
        bool playerSpawned = false;
        
        for (int i = 0; i < currentLevel.possibleSpawnPoints.Length; i++)
        {
            int r = Random.Range(0, 100);

            if (r < brickSpawnRate && r > enemySpawnRate)
            {
                Brick b = Instantiate(GlobalData.Instance.GetBrickPrefab(),
                    currentLevel.possibleSpawnPoints[i].position, Quaternion.identity).GetComponent<Brick>();
                
                b.Initialize(brickSprite);
            }
            else if (r < enemySpawnRate)
            {
                Enemy e = Instantiate(GlobalData.Instance.GetEnemyPrefab(),
                    currentLevel.possibleSpawnPoints[i].position, Quaternion.identity).GetComponent<Enemy>();
            }
            else if(!playerSpawned)
            {
                Instantiate(GlobalData.Instance.GetPlayerPrefab(), currentLevel.possibleSpawnPoints[i].position,
                    Quaternion.identity);
                playerSpawned = true;
            }
        }
    }

    private LevelData GetRandomLevel()
    {
        return levelPrefabs[Random.Range(0, levelPrefabs.Length)].GetComponent<LevelData>();
    }
}
