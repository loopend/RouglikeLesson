using System;
using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.Pool;

using System.Collections;

using UnityEngine;


namespace Assets.Scripts.GameCore.LevelSystem
{
    [Serializable]
    public class Level : IActivate
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

        public void Activate()
        {
            for (int i = 0; i < _enemySpawners.Count; i++)
            {
                _enemySpawners[i].Activate();
            }
        }

        public void Deactivate()
        {
            for (int i = 0; i < _enemySpawners.Count; i++)
            {
                _enemySpawners[i].Deactivate();
            }
        }
    }
}
