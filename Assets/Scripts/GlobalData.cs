using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData Instance;

    [SerializeField] private Sprite[] brickSprites;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bombExplosionPrefab;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject brickExplosionPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxBombCount;
    [SerializeField] private float bombTimeToExplode;
    
    private void Awake()
    {
        Instance =  this;
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
}
