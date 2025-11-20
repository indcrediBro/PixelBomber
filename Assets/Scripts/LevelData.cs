using System;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Transform[] possibleSpawnPoints;

    private void Awake()
    {
        foreach (Transform spawnPoint in possibleSpawnPoints)
        {
            spawnPoint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
