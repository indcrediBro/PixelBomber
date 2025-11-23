using UnityEngine;

public static class GridUtilities
{
    public static Vector2 SnapToGrid(Vector2 pos, float cellSize = 0.5f)
    {
        float x = Mathf.Round(pos.x / cellSize) * cellSize;
        float y = Mathf.Round(pos.y / cellSize) * cellSize;
        return new Vector2(x, y);
    }
}