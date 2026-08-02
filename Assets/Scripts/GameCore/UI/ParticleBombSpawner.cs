using System.Collections;
using Assets.Scripts.GameCore.Pool;
using UnityEngine;

namespace Assets.Scripts.GameCore.UI
{
    public class ParticleBombSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;
        private readonly WaitForSeconds _lifetime = new WaitForSeconds(1f);

        public void Spawn(Vector3 position)
        {
            GameObject particle = _objectPool.GetFromPool();
            particle.transform.SetParent(transform);
            particle.transform.position = position;

            if (particle.TryGetComponent(out ParticleSystem particleSystem))
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                particleSystem.Play(true);
            }

            StartCoroutine(ReturnToPool(particle));
        }

        private IEnumerator ReturnToPool(GameObject particle)
        {
            yield return _lifetime;
            if (particle != null)
                particle.SetActive(false);
        }
    }
}
