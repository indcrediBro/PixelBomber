using System;
using System.Collections;
using UnityEngine;

public class Bomb: MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private TMPro.TMP_Text timerText;
    private float timeToExplode; 

    public void Init(PlayerController _playerController)
    {
        playerController = _playerController;
        timeToExplode = GlobalData.Instance.GetTimeToExplode();
    }

    private void Start()
    {
        // Destroy(this,timeToExplode);
    }

    private void Update()
    {
        timeToExplode-= Time.deltaTime;
        
        timerText.text = timeToExplode.ToString("0");
        if (timeToExplode <= 0)
        {
            playerController.BombExploded(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        // playerController.BombExploded(gameObject);
    }
}