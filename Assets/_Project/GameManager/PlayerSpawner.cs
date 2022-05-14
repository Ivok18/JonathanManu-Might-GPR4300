using UnityEngine;
using Might.MapGeneration;
using System.Collections.Generic;
using System;

namespace Might.GameManager
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GenerationBehaviour generation;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private int spawnOffset;

        public delegate void PlayerSpawnedCallback(Transform player);
        public static event PlayerSpawnedCallback OnPlayerSpawnCallback;

        private void OnEnable()
        {
            GenerationBehaviour.OnGenerationEndedCallback += HandleGenerationEnd;
        }

        private void OnDisable()
        {
            GenerationBehaviour.OnGenerationEndedCallback -= HandleGenerationEnd;
        }

        private void HandleGenerationEnd()
        {
            //SpawnPlayer();       
        }

        public void SpawnPlayer()
        {
            MapProcessor mapProcessor = generation.GetComponent<MapProcessor>();
            NeighbourTilesTracker tracker = generation.GetComponent<NeighbourTilesTracker>();

            foreach (List<Coord> region in mapProcessor.ListOfRegions)
            {

                if (region.Count > generation.CaveMinimumSize)
                {
                    foreach (Coord tile in region)
                    {
                        if (generation.Map[tile.x, tile.y] == 2)
                        {
                            int surroundingCaveCount = tracker.GetSurroundingCaveCount(tile.x, tile.y);
                            if (surroundingCaveCount >= 8)
                            {
                                GameObject player = Instantiate(playerPrefab, new Vector3(tile.x + spawnOffset, tile.y, 0), Quaternion.identity);
                                OnPlayerSpawnCallback?.Invoke(player.transform);
                                return;
                            }

                        }

                    }
                }
            }
    

        }
    }
}
