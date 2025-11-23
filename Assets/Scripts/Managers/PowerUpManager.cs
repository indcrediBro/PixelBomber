using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager>
{
    [SerializeField] GameObject[] powerUpPrefab;

    public GameObject GetRandomPowerUp()
    {
        return powerUpPrefab[Random.Range(0, powerUpPrefab.Length)];
    }
}
