using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bombParent,bombDropPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;
    private bool holdingBomb;

    private List<GameObject> activeBombs;
    private GameObject currentBomb;
    private const string SPEED = "Speed";
    private const string HOLDING = "Holding";

    private void Start()
    {
        activeBombs =  new List<GameObject>();
    }

    void Update()
    {
        GetInput();
        UpdateAnimation();
        HandleBomb();
    }

    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * GlobalData.Instance.GetPlayerMoveSpeed() * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        if (animator == null) return;
        
        animator.SetFloat(SPEED, movement.magnitude);
        animator.SetBool(HOLDING,holdingBomb);
    }

    private void HandleBomb()
    {
        if (Input.GetButtonDown("Jump") && activeBombs.Count < GlobalData.Instance.GetMaxBombCountAllowed())
        {
            SpawnBomb();            
        }

        if (holdingBomb && Input.GetButtonUp("Jump") && currentBomb)
        {
            DropBomb();
        }
    }

    private void SpawnBomb()
    {
        Bomb b = Instantiate(GlobalData.Instance.GetBombPrefab(),bombParent.transform.position,Quaternion.identity, bombParent).GetComponent<Bomb>();
        b.Init(this);
        activeBombs.Add(b.gameObject);
        currentBomb = b.gameObject;
        holdingBomb = true;
    }

    private void DropBomb()
    {
        currentBomb.transform.position = bombDropPos.position;
        currentBomb.transform.parent = null;
        currentBomb = null;
        holdingBomb = false;
    }

    public void BombExploded(GameObject _bomb)
    {
        activeBombs.Remove(_bomb);
    }
}