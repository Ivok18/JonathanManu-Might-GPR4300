using Might.MapGeneration;
using System.Collections.Generic;
using UnityEngine;

namespace Might.GameManager
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GenerationBehaviour generation;
        //[SerializeField] private GameObject enemyPrefab;

        public delegate void EnemySpawnedCallback(Transform enemy);
        public static event EnemySpawnedCallback OnEnemySpawnedCallback;
   
        public void SpawnEnemy(GameObject enemyPrefab)
        {
            MapProcessor mapProcessor = generation.GetComponent<MapProcessor>();
            NeighbourTilesTracker tracker = generation.GetComponent<NeighbourTilesTracker>();
            int width = generation.Width;
            int height = generation.Height;

            for (int x = width- 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    if (generation.Map[x, y] == 2)
                    {
                        List<Coord> region = mapProcessor.GetRegionTiles(generation.Map, x, y);
                        int caveCount = tracker.GetSurroundingCaveCount(x, y);
                        if (caveCount >= 8)
                        {
                            GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
                            OnEnemySpawnedCallback?.Invoke(enemy.transform);
                            return;
                        }
                        //72 -> width 40-> height 39->fill percent 100 -> cave minomum size
                    }
                }
            }

        }
    }
}
