
using UnityEngine;

public class ExtraBomb : MonoBehaviour, PowerUp
{
    public void ApplyPowerUp()
    {
        GiveExtraBomb();
    }

    private void GiveExtraBomb()
    {
        Debug.Log("Extending Bombs");
        GlobalData.Instance.IncreaseBombLimit();
        Destroy(gameObject);
    }
}