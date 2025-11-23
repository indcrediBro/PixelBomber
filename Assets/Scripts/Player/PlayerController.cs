using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bombParent,bombDropPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float detachForce = 5f;
    [SerializeField] private Transform bodyPartsParent;
    
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

    private void OnEnable()
    {
        UIManager.Instance.UpdateLivesUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit") && GlobalData.Instance.spawnedEnemies.Count == 0)
        {
            transform.SetParent(null);
            gameObject.SetActive(false);
            LevelManager.Instance.ClearLevel();
        }

        if (other.CompareTag("PowerUp"))
        {
            other.GetComponent<PowerUp>().ApplyPowerUp();
        }
    }
    
    public void DetachBodyParts()
    {
        for (int i = 0; i < bodyPartsParent.childCount; i++)
        {
            Transform child = bodyPartsParent.GetChild(i);
            child.SetParent(null);
            child.gameObject.SetActive(true);

            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * detachForce, ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);
    }
}