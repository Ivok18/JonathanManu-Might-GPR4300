using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class MapClearer : MonoBehaviour
    {
        private GenerationBehaviour generationBehaviour;
        
        public void ClearMap()
        {
            generationBehaviour = GetComponent<GenerationBehaviour>();
            generationBehaviour.GroundTilemap.ClearAllTiles();
            generationBehaviour.CaveTilemap.ClearAllTiles();        
        }
    }
}
