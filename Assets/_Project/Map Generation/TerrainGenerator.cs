using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        private GenerationBehaviour generation;
 
        public int[] PerlinHeightList { get; set; }
        public Coord ObstacleCoord { get; set; }

        public int[,] GenerateTerrain(int[,] map)
        {

            generation = GetComponent<GenerationBehaviour>();
            PerlinHeightList = new int[generation.Width];
            ObstacleCoord = new Coord(0, 0);

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
                

                if(x == width - width / 2 - 2)
                {
                    ObstacleCoord.x = x;
                    ObstacleCoord.y = PerlinHeightList[x] - PerlinHeightList[x] / 2;
                }
                
          
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

                    if(x == ObstacleCoord.x && y == ObstacleCoord.y)
                    {
                        int obstaclePosX = ObstacleCoord.x;
                        int obstaclePosY = ObstacleCoord.y;
                        map[obstaclePosX, obstaclePosY] = -1;
                    }
                    
                }
            }



            return map;
        }
    }
}
