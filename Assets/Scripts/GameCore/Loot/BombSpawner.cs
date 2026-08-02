using Assets.Scripts.GameCore.Pool;
using UnityEngine;

namespace Assets.Scripts.GameCore.Loot
{
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] [Range(0f, 100f)] private float _dropChance = 25f;

        public void TrySpawn(Vector3 position)
        {
            if (Random.Range(0f, 100f) <= _dropChance)
                Spawn(position);
        }

        public void Spawn(Vector3 position)
        {
            GameObject bomb = _objectPool.GetFromPool();
            bomb.transform.SetParent(transform);
            bomb.transform.position = position;
        }
    }
}
