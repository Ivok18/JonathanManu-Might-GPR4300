using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class MapClearer : MonoBehaviour
    {
        private GenerationBehaviour generation;
        
        public void ClearMap()
        {
            generation = GetComponent<GenerationBehaviour>();

            generation.GroundTilemap.ClearAllTiles();
            generation.CaveTilemap.ClearAllTiles();
            generation.ObstacleTilemap.ClearAllTiles();
        }
    }
}
