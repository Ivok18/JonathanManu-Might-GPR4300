using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        private GenerationBehaviour generation;
 
        public int[] PerlinHeightList { get; set; }

        public int[,] GenerateTerrain(int[,] map)
        {
            generation = GetComponent<GenerationBehaviour>();
            PerlinHeightList = new int[generation.Width];

            #region GENERATION VALUES USED IN THIS FUNCTION
            float seed = generation.Seed;
            float smoothness = generation.Smoothness;
            int width = generation.Width;
            int height = generation.Height;
            int randomFillPercent = generation.RandomFillPercent;
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
