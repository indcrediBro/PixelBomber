using UnityEngine;

public class ExtraRange: MonoBehaviour, PowerUp
{
    public void ApplyPowerUp()
    {
        GiveExtraExplosionRange();
    }

    private void GiveExtraExplosionRange()
    {
        ScoreManager.Instance.AddScore(500, transform.position);
        GlobalData.Instance.IncreaseBombExplosionRangeLimit();
        Destroy(gameObject);
    }
}