using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator: MonoBehaviour
{
    [SerializeField] GameObject[] levelPrefabs;
    [SerializeField] private float brickSpawnRate;
    [SerializeField] private float enemySpawnRate;
    [HideInInspector] public GameObject activeLevel;
    
    private void Start()
    {
    }

    public void GenerateLevel()
    {
        if (activeLevel != null)
        {
            Destroy(activeLevel);
        }
        LevelData currentLevel = GetRandomLevel();
        activeLevel = Instantiate(currentLevel, Vector3.zero, Quaternion.identity, transform).gameObject;

        Sprite brickSprite = GlobalData.Instance.GetRandomBrickSprite();
        List<Transform> spawnedBricks = new List<Transform>();
        
        GameObject bricks = new GameObject("Bricks");
        bricks.transform.SetParent(activeLevel.transform);
        
        GameObject enemies = new GameObject("Enemies");
        enemies.transform.SetParent(activeLevel.transform);
        GlobalData.Instance.spawnedEnemies = new List<Enemy>();
        
        for (int i = 0; i < currentLevel.possibleSpawnPoints.Length; i++)
        {
            int r = Random.Range(0, 100);

            if (r < brickSpawnRate && r > enemySpawnRate)
            {
                Brick b = Instantiate(GlobalData.Instance.GetBrickPrefab(),
                    currentLevel.possibleSpawnPoints[i].position, Quaternion.identity,bricks.transform).GetComponent<Brick>();
                
                b.Initialize(brickSprite);
                spawnedBricks.Add(b.transform);
            }
            else if (r < enemySpawnRate)
            {
                Enemy e = Instantiate(GlobalData.Instance.GetEnemyPrefab(),
                        currentLevel.possibleSpawnPoints[i].position, Quaternion.identity, enemies.transform)
                    .GetComponent<Enemy>();

                GlobalData.Instance.spawnedEnemies.Add(e);
            }
        }
        
        int rx = Random.Range(0, spawnedBricks.Count);
        if (GlobalData.Instance.activeExit == null)
        {
            GameObject x = Instantiate(GlobalData.Instance.GetExitPrefab(), spawnedBricks[rx].position,
                Quaternion.identity);
            x.transform.SetParent(spawnedBricks[rx].transform);
            GlobalData.Instance.activeExit = x;
        }
        else
        {
            GlobalData.Instance.activeExit.SetActive(false);
            GlobalData.Instance.activeExit.transform.SetParent(spawnedBricks[rx].transform);
            GlobalData.Instance.activeExit.transform.localPosition = Vector3.zero;
        }
        
    }

    private LevelData GetRandomLevel()
    {
        return levelPrefabs[Random.Range(0, levelPrefabs.Length)].GetComponent<LevelData>();
    }

    public void ToggleLevelVisiblility()
    {
        activeLevel.SetActive(!activeLevel.activeSelf);
    }
}
