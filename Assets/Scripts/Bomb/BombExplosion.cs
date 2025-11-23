using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [Header("Beam renderers: Up, Down, Left, Right")]
    public LineRenderer[] beams; // size 4 in Inspector

    public float duration = 0.25f;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    public void DrawBeam(Vector2 dir, float length)
    {
        LineRenderer lr = GetRenderer(dir);

        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + (Vector3)(dir * length));
        lr.enabled = true;
    }

    private LineRenderer GetRenderer(Vector2 dir)
    {
        if (dir == Vector2.up) return beams[0];
        if (dir == Vector2.down) return beams[1];
        if (dir == Vector2.left) return beams[2];
        return beams[3]; // right
    }
}