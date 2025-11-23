using UnityEngine;

public class Bomb : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private TMPro.TMP_Text timerText;
    private float timeToExplode;
    public float cellSize = 0.5f;

    public LayerMask obstacleMask; 
    public LayerMask damageMask;   

    [SerializeField] private BombExplosion explosionVisualPrefab;
    private bool isExploding = false;

    public void Init(PlayerController _playerController)
    {
        playerController = _playerController;
        timeToExplode = GlobalData.Instance.GetTimeToExplode();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(transform.parent == null)
            transform.position = GridUtilities.SnapToGrid(transform.position, cellSize);
            
        timeToExplode -= Time.deltaTime;
        timerText.text = timeToExplode.ToString("0");
        if (timeToExplode <= 0)
        {
            playerController.BombExploded(gameObject);
            GameEvents.BombExploded();
            AudioManager.Instance.PlaySound("Explode");
            Explode();
            Destroy(gameObject);
        }

    }

    private void Explode()
    {
        if (isExploding) return;
            
        isExploding = true;
        
        Vector2[] dirs =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        BombExplosion visual = Instantiate(explosionVisualPrefab, transform.position, Quaternion.identity);

        foreach (var dir in dirs)
        {
            float length = CastExplosionRay(dir);
            visual.DrawBeam(dir, length);
            DamageAlongRay(dir, length);
        }

        Destroy(gameObject);
    }

    private float CastExplosionRay(Vector2 dir)
    {
        float maxDist = GlobalData.Instance.explosionLength * cellSize;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxDist, obstacleMask);

        if (hit.collider)
        {
            return hit.distance;
        }

        return maxDist;
    }

    private void DamageAlongRay(Vector2 dir, float dist)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, dist, damageMask);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }

            if (hit.collider.CompareTag("Brick"))
            {
                hit.collider.GetComponent<Brick>().ExplodeBrick();
            }

            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerController>().DetachBodyParts();
                Destroy(hit.collider.gameObject);
                GameEvents.PlayerKilled();
            }

            if (hit.collider.CompareTag("Bomb") && hit.collider != GetComponent<Collider2D>())
            {
                hit.collider.GetComponent<Bomb>().Explode();
            }
        }
    }
}