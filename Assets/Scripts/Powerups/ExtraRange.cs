using UnityEngine;

public class ExtraRange: MonoBehaviour, PowerUp
{
    public void ApplyPowerUp()
    {
        GiveExtraExplosionRange();
    }

    private void GiveExtraExplosionRange()
    {
        Debug.Log("Extending Range");
        GlobalData.Instance.IncreaseBombExplosionRangeLimit();
        Destroy(gameObject);
    }
}