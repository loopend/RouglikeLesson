
using UnityEngine;

public class EnemyCollisionBee : MonoBehaviour
{
    [SerializeField] private float _damageTick = 5f;
    [SerializeField] private float _Duration = 5f;    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out var player))
        {
            player.ApplyDamageOverTime(_damageTick,_Duration);
            gameObject.SetActive(false);
        }
    }
}
