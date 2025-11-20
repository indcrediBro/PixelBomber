using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform brickExplodeEffect;
    
    public void Initialize(Sprite _sprite)
    {
        spriteRenderer.sprite = _sprite;
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
