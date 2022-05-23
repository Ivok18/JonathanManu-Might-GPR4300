using System;
using UnityEngine;

namespace Might.WaveSystem
{
    [Serializable]
    public class EnemySpawnerData
    {
        public int noOfEnemiesToSpawn;
        [HideInInspector] public float nextSpawnTime;
        public float spawnInterval;
        public Transform[] typeOfEnemies;
    }
}
