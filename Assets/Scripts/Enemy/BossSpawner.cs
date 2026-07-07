using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.ExperienceSystem;
using Assets.Scripts.GameCore.Pool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private int _bossLevelInterval = 10;
        [SerializeField] private Transform _minPos;
        [SerializeField] private Transform _maxPos;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private ObjectPool _bossPool;

        private ExperienceSystem _experienceSystem;
        private GetRandomSpawnPoint _spawnPoint;

        private void OnEnable() => _experienceSystem.OnLevelUp += TrySpawnBoss;
        private void OnDisable() => _experienceSystem.OnLevelUp -= TrySpawnBoss;

        private void TrySpawnBoss(int level)
        {
            if (level % _bossLevelInterval != 0)
                return;

            GameObject boss = _bossPool.GetFromPool();
            boss.transform.SetParent(_enemyContainer);
            boss.transform.position = _spawnPoint.GetRandomPoint(_minPos, _maxPos);
        }

        [Inject]
        private void Construct(ExperienceSystem experienceSystem, GetRandomSpawnPoint spawnPoint)
        {
            _experienceSystem = experienceSystem;
            _spawnPoint = spawnPoint;
        }
    }
}
