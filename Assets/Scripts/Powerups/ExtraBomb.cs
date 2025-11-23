
using UnityEngine;

public class ExtraBomb : MonoBehaviour, PowerUp
{
    public void ApplyPowerUp()
    {
        GiveExtraBomb();
    }

    private void GiveExtraBomb()
    {
        ScoreManager.Instance.AddScore(500, transform.position);
        GlobalData.Instance.IncreaseBombLimit();
        Destroy(gameObject);
    }
}