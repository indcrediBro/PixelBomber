using UnityEngine;

public class ExtraLife : MonoBehaviour, PowerUp
{
    public void ApplyPowerUp()
    {
        GiveExtraLife();
    }

    private void GiveExtraLife()
    {
        Debug.Log("Extending Life");
        GlobalData.Instance.IncreaseCurrentHealthLimit();
        Destroy(gameObject);
    }
}