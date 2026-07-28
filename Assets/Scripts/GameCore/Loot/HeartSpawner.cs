using Assets.Scripts.GameCore.Pool;
using UnityEngine;

namespace Assets.Scripts.GameCore.Loot
{
    public class HeartSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private int _killsPerDrop = 5;

        private int _killCount;

        public void RegisterKill(Vector3 position)
        {
            _killCount++;
            if (_killCount % _killsPerDrop == 0)
                Spawn(position);
        }

        public void Spawn(Vector3 position)
        {
            GameObject heart = _objectPool.GetFromPool();
            heart.transform.SetParent(transform);
            heart.transform.position = position;
        }
    }
}
