using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}
