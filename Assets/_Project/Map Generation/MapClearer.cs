using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class MapClearer : MonoBehaviour
    {
        private GenerationBehaviour generationBehaviour;

        private void Awake()
        {
            generationBehaviour = GetComponent<GenerationBehaviour>();
        }
        
        public void ClearMap()
        {
            generationBehaviour.GroundTilemap.ClearAllTiles();
            generationBehaviour.CaveTilemap.ClearAllTiles();
        }
    }
}
