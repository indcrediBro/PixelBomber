using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] Color[] colors =  new Color[4];
    [SerializeField] int colorIndex = 0;
    [SerializeField] private float changeRate = 1f;
    private float timer;
    [SerializeField] SpriteRenderer[] spriteRenderers;
    
    private void Awake()
    {
        colors[0] = Color.white;
        colors[1] = Color.blue;
        colors[2] = Color.red;
        colors[3] = Color.yellow;
        
        colorIndex = Random.Range(0, colors.Length);
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= changeRate)
        {
            timer = 0;
            colorIndex++;
            if (colorIndex >= colors.Length) 
                colorIndex = 0;
                
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = colors[colorIndex];
            }
        }
    }
}