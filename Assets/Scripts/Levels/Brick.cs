using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
    [SerializeField] private int powerUpSpawnRate;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Transform brickExplodeEffect;
    
    public void Initialize(Sprite _sprite)
    {
        spriteRenderer.sprite = _sprite;
        brickExplodeEffect =
            Instantiate(GlobalData.Instance.GetBrickExplosionPrefab(), transform.position, Quaternion.identity)
                .transform;
    }
    
    public void ExplodeBrick()
    {
        if (brickExplodeEffect == null) return;

        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.DetachChildren();
        }

        int r = Random.Range(0, 100);
        if (r <= powerUpSpawnRate)
        {
            GameObject p = Instantiate(PowerUpManager.Instance.GetRandomPowerUp(),transform.position,Quaternion.identity);
            p.transform.SetParent(transform.parent);
        }
        AudioManager.Instance.PlaySound("Brick");
        brickExplodeEffect.SetParent(null);
        brickExplodeEffect.gameObject.SetActive(true);
        
        Destroy(gameObject);
    }
}
