using System;
using System.Collections;
using UnityEngine;

public class Bomb: MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private TMPro.TMP_Text timerText;
    private float timeToExplode;
    private GameObject bombExplosionEffect;

    public void Init(PlayerController _playerController)
    {
        playerController = _playerController;
        timeToExplode = GlobalData.Instance.GetTimeToExplode();
        bombExplosionEffect = Instantiate(GlobalData.Instance.GetBombExplosionPrefab(), transform.position,
            Quaternion.identity,transform);
    }
    
    private void Update()
    {
        timeToExplode-= Time.deltaTime;
        
        timerText.text = timeToExplode.ToString("0");
        if (timeToExplode <= 0)
        {
            playerController.BombExploded(gameObject);
            bombExplosionEffect.SetActive(true);
            bombExplosionEffect.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
    
    
}