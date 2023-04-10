using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject soundManager;
    public GameObject musicManager;
    public Image healthBar;
    public float health = 100f;
    public int damageAmount = 10;

    private void Awake()
    {
        health = 100f;
        healthBar.fillAmount = health / 100;
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            return;


        health -= damage;
        healthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            FindObjectOfType<PlayerMovement>().Die();

            soundManager.SetActive(false);
            musicManager.SetActive(false);
        }
    }

}
