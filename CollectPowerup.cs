using UnityEngine;

public class CollectPowerup : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject PowerUp;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private HealthBar healthBarScript;
    #endregion

    #region CustomEvents
    public void JumpAmount_Gem()
    {
        Destroy(PowerUp);
        playerMovement.jumpAmount = 2;
    }

    public void RestoreHealth_Cherry(int addHealth)
    {
        Destroy(PowerUp);
        if (healthBarScript.health < 100)
        {
            healthBarScript.health += addHealth;
            healthBarScript.healthBar.fillAmount += (healthBarScript.healthBar.fillAmount + addHealth) / 100;
        }

        if (healthBarScript.health > 100)
        {
            healthBarScript.health = 100;
        }
    }

    #endregion
}

