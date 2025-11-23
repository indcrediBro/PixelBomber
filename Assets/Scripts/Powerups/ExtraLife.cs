using UnityEngine;

public class ExtraLife : MonoBehaviour, PowerUp
{
    public void ApplyPowerUp()
    {
        GiveExtraLife();
    }

    private void GiveExtraLife()
    {
        ScoreManager.Instance.AddScore(500, transform.position);
        GlobalData.Instance.IncreaseCurrentHealthLimit();
        Destroy(gameObject);
    }
}