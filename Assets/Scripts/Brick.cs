using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform brickExplodeEffect;
    
    private void Start()
    {
        spriteRenderer.sprite = GlobalData.Instance.GetRandomBrickSprite();
        brickExplodeEffect =
            Instantiate(GlobalData.Instance.GetBrickExplosionPrefab(), transform.position, Quaternion.identity)
                .transform;
    }

    private void OnDisable()
    {
        if (brickExplodeEffect == null) return;
        
        brickExplodeEffect.SetParent(null);
        brickExplodeEffect.gameObject.SetActive(true);
    }
}
