using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Enemy:MonoBehaviour
{
    [SerializeField] private float bombDetectionRange = 2f;
    [SerializeField] private float obstacleDetectionRange = 0.25f;
    [SerializeField] private LayerMask bombLayer;
    [SerializeField] private LayerMask avoidLayers; //blocks, bricks, bombs
    [SerializeField] private LayerMask playerLayer;
    
    private List<Vector2> directions;
    private Vector2 currentDirection;
    
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackSpeed;
    private float originalSpeed;
    
    private void Awake()
    {
        InitializeDirections();
        originalSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.currentState != GameState.Game) return;
        
        SpeedCheck();
        DecideDirection();
        MoveEnemy();
    }

    private void InitializeDirections()
    {
        directions = new List<Vector2>();
        directions.Add(new Vector2(1, 0));
        directions.Add(new Vector2(-1, 0));
        directions.Add(new Vector2(0, -1));
        directions.Add(new Vector2(0, 1));

        GetValidDirection();
    }
    
    private void SpeedCheck()
    {
        if (moveSpeed > originalSpeed)
        {
            moveSpeed -= Time.fixedDeltaTime;
        }

        if (moveSpeed < originalSpeed)
        {
            moveSpeed = originalSpeed;
        }
    }

    private void MoveEnemy()
    {
        rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
    }
    
    private void DecideDirection()
    {
        if (!PlayerDetected() && BombDetected())
        {
            GetValidDirection();
            if(moveSpeed == originalSpeed)
                moveSpeed += attackSpeed;
        }
        else if (PlayerDetected() && !BombDetected())
        {
            if(moveSpeed == originalSpeed)
                moveSpeed += attackSpeed;
        }
        else if (PlayerDetected() && BombDetected())
        {
            GetValidDirection();
            if(moveSpeed == originalSpeed)
                moveSpeed += attackSpeed;
        }
        else if (ObstacleDetected())
        {
            GetValidDirection();
        }
    }

    private void GetValidDirection()
    {
        List<Vector2> validDirections = GetAllPossibleDirections();
        int r = Random.Range(0, validDirections.Count);
        if (validDirections.Count == 0)
        {
            //Do Nothing
        }
        else if (validDirections.Count > 0)
        {
            if (validDirections[r] != currentDirection)
            {
                currentDirection = validDirections[r];
            }
            else
            {
                currentDirection *= -1;
            }
        }
    }

    private List<Vector2> GetAllPossibleDirections()
    {
        List<Vector2> validDirections = new List<Vector2>();
        foreach (Vector2 dir in directions)
        {
            if (!Physics2D.Raycast(transform.position, dir, obstacleDetectionRange, avoidLayers))
            {
                validDirections.Add(dir);
            }
        }
        
        return validDirections;
    }
    
    private bool ObstacleDetected()
    {
        if (Physics2D.Raycast(transform.position, currentDirection, obstacleDetectionRange, avoidLayers))
        {
            return true;
        }
        return false;
    }

    private bool PlayerDetected()
    {
        if (Physics2D.Raycast(transform.position, currentDirection, bombDetectionRange, playerLayer))
        {
            return true;
        }
        return false;
    }

    private bool BombDetected()
    {
        if (Physics2D.Raycast(transform.position, currentDirection, bombDetectionRange, bombLayer))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            other.collider.GetComponent<PlayerController>().DetachBodyParts();
            GameEvents.PlayerKilled();
        }
    }

    private void OnDestroy()
    {
        GlobalData.Instance.spawnedEnemies.Remove(this);
    }
}