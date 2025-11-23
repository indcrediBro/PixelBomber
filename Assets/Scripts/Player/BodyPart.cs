using DG.Tweening;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private float timeToVanish;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private void OnEnable()
    {
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0, timeToVanish).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
