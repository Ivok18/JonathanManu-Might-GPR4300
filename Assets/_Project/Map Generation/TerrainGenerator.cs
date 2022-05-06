using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        private GenerationBehaviour generationBehaviour;
 
        public int[] PerlinHeightList { get; set; }
      
        private void Awake()
        {
            generationBehaviour = GetComponent<GenerationBehaviour>();
        }

        private void Start()
        {
            PerlinHeightList = new int[generationBehaviour.Width];
        }
        public int[,] GenerateTerrain(int[,] map)
        {
            #region GENERATION VALUES USED IN THIS FUNCTION
            float seed = generationBehaviour.Seed;
            float smoothness = generationBehaviour.Smoothness;
            int width = generationBehaviour.Width;
            int height = generationBehaviour.Height;
            int randomFillPercent = generationBehaviour.RandomFillPercent;
            #endregion 

            System.Random pseudoRandom = new System.Random(seed.GetHashCode());
            int perlinHeight;
            for (int x = 0; x < width; x++)
            {
                perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height / 2);
                perlinHeight += height / 2;
                PerlinHeightList[x] = perlinHeight;
                for (int y = 0; y < PerlinHeightList[x]; y++)
                {
                    if (pseudoRandom.Next(1, 100) < randomFillPercent)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = 2;
                    }
                }
            }
            return map;
        }
    }
}
