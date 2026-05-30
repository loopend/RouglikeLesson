using System.Collections;
using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] [Range(0f, 1f)] private float _damageSoundVolume = 0.35f;

    private WaitForSeconds _tick = new WaitForSeconds(1f);

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        PlayDamageSound();
        if (CurrentHealth <= 0)
            gameObject.SetActive(false);
    }

    private void PlayDamageSound()
    {
        if (_damageSound != null)
            AudioSource.PlayClipAtPoint(_damageSound, transform.position, _damageSoundVolume);
    }
    public void Burn(float damage) => StartCoroutine(routine:StartBurn(damage));
    private IEnumerator StartBurn(float damage)
    {
        if (gameObject.activeSelf == false)
        {
            yield break;
            //float tickDamage = damage / 3f;
            //if (tickDamage < 1f)
            //    tickDamage = 1f;
            //float roundDamage = Mathf.Round(tickDamage);
            //for (int i = 0; i < 5; i++)
            //{
            //    TakeDamage(roundDamage);
            //    yield return _tick;
            //}
        }
    }
  
}
