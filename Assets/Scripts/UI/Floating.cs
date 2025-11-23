using UnityEngine;
using DG.Tweening;

public class Floating : MonoBehaviour
{
    public float floatStrength = 0.5f;   // How high it moves
    public float duration = 1.5f;        // Time to move up/down
    public Ease easeType = Ease.InOutSine;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;

        // Looping up and down motion
        transform.DOLocalMoveY(startPos.y + floatStrength, duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
    }
}
