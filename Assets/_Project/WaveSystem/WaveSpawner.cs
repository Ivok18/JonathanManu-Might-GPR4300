using Might.Entity.Enemy;
using Might.GameManager;
using Might.MapGeneration;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Might.WaveSystem
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveData[] waves;    
        private int currentWaveIndex;

        public delegate void WaveEndedCallback();
        public static event WaveEndedCallback OnWaveEndedCallback;

        public WaveData CurrentWave => waves[currentWaveIndex];

        public EnemySpawnerData Spawner => CurrentWave.spawner;

        private void OnEnable()
        {
            GenerationBehaviour.OnGenerationEndedCallback += HandleGenerationEnd;
            EnemyHealth.OnEnemyDiedCallback += HandleEnemyDied;         
        }

        private void OnDisable()
        {
            GenerationBehaviour.OnGenerationEndedCallback -= HandleGenerationEnd;
            EnemyHealth.OnEnemyDiedCallback -= HandleEnemyDied;
        }

        public void HandleEnemyDied()
        {
            CurrentWave.totalEnemies--;

            if(CurrentWave.totalEnemies <= 0)
            {
                CurrentWave.waveState = WaveState.Inactive;
                if(currentWaveIndex + 1 < waves.Length)
                {
                    currentWaveIndex++;
                }
                else
                {
                    //Load Win Scene
                }

                OnWaveEndedCallback?.Invoke();              
            }
        }

        public void HandleGenerationEnd()
        {
            CurrentWave.waveState = WaveState.Active;
        }

        private void Start()
        {
            foreach(WaveData wave in waves)
            {
                wave.totalEnemies = wave.spawner.noOfEnemiesToSpawn;
            }
        }
        private void Update()
        {
            switch (CurrentWave.waveState)
            {
                case WaveState.Active:
                    SpawnWave();
                    break;
                case WaveState.Inactive:
                    break;
                default:
                    break;
            }
        }

        public void SpawnWave()
        {
            if(Spawner.nextSpawnTime < Time.time && Spawner.noOfEnemiesToSpawn > 0)
            {
                //Instantiate random enemy 
                int randomEnemyIndex = Random.Range(0, Spawner.typeOfEnemies.Length);
                GameObject randomEnemy = Spawner.typeOfEnemies[randomEnemyIndex].gameObject;
                enemySpawner.SpawnEnemy(randomEnemy);
                Spawner.noOfEnemiesToSpawn--;

                //Reset spawn time
                Spawner.nextSpawnTime = Time.time + Spawner.spawnInterval;

            }
        }
    }
}
