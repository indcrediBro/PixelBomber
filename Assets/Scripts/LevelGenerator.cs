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
    }

    public void GenerateLevel()
    {
        LevelData currentLevel = GetRandomLevel();
        activeLevel = Instantiate(currentLevel, Vector3.zero, Quaternion.identity, transform).gameObject;
        Sprite brickSprite = GlobalData.Instance.GetRandomBrickSprite();
        bool playerSpawned = false;
        GameObject bricks = new GameObject("Bricks");
        bricks.transform.SetParent(activeLevel.transform);
        GameObject enemies = new GameObject("Enemies");
        enemies.transform.SetParent(activeLevel.transform);

        for (int i = 0; i < currentLevel.possibleSpawnPoints.Length; i++)
        {
            int r = Random.Range(0, 100);

            if (r < brickSpawnRate && r > enemySpawnRate)
            {
                Brick b = Instantiate(GlobalData.Instance.GetBrickPrefab(),
                    currentLevel.possibleSpawnPoints[i].position, Quaternion.identity,bricks.transform).GetComponent<Brick>();
                
                b.Initialize(brickSprite);
            }
            else if (r < enemySpawnRate)
            {
                Enemy e = Instantiate(GlobalData.Instance.GetEnemyPrefab(),
                    currentLevel.possibleSpawnPoints[i].position, Quaternion.identity,enemies.transform).GetComponent<Enemy>();
            }
            else if(!playerSpawned)
            {
                Instantiate(GlobalData.Instance.GetPlayerPrefab(), currentLevel.possibleSpawnPoints[i].position,
                    Quaternion.identity,activeLevel.transform);
                playerSpawned = true;
            }
        }
    }

    private LevelData GetRandomLevel()
    {
        return levelPrefabs[Random.Range(0, levelPrefabs.Length)].GetComponent<LevelData>();
    }
}
