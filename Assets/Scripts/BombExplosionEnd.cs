using System;
using UnityEngine;

public class BombExplosionEnd : MonoBehaviour
{
    [SerializeField] private bool destroyable = true;


    // private void OnEnable()
    // {
    //     RaycastHit2D hit = Physics2D.CircleCast(transform.position,0.1f, Vector2.up);
    //     if (hit.collider != null)
    //     {
    //         
    //     }
    // }
    //
    // private void Detect(Collider2D collider)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyable || other.tag == "Block")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Brick")
        {
            Destroy(other.gameObject);
        }
        
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
