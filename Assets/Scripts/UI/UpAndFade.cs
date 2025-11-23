using UnityEngine;
using DG.Tweening;
using TMPro;

public class UpAndFade : MonoBehaviour
{
    public float moveUpDistance = 1f;
    public float duration = 1f;
    public Ease moveEase = Ease.OutQuad;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * moveUpDistance;

        // Create tween sequence
        Sequence seq = DOTween.Sequence();

        seq.Join(transform.DOMove(endPos, duration)
            .SetEase(moveEase));

        seq.Join(text.DOFade(0f, duration));

        seq.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
