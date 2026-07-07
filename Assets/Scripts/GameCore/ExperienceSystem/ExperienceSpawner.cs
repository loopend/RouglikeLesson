using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.Pool;
using UnityEngine;

namespace Assets.Scripts.GameCore.ExperienceSystem
{
    public class ExperienceSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;



        public void Spawn(Vector3 position)
        {
            var experience = _objectPool.GetFromPool();
            experience.transform.SetParent(transform);
            experience.transform.position = position;
        }


    }
}
