using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;
    
    private const string SPEED = "Speed";
    
    void Update()
    {
        GetInput();
        UpdateAnimation();
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    
    void UpdateAnimation()
    {
        if (animator == null) return;
        
        animator.SetFloat(SPEED, movement.magnitude);
    }
}