using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float Interval;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        InvokeRepeating("DealDamage", 1f, Interval);
    }

    void DealDamage()
    {
        if (playerHealth != null && playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
