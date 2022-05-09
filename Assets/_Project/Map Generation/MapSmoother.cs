using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class MapSmoother : MonoBehaviour
    {
        private GenerationBehaviour generation;
        private TerrainGenerator terrainGenerator;

        public void SmoothMap()
        {
            generation = GetComponent<GenerationBehaviour>();
            terrainGenerator = GetComponent<TerrainGenerator>();
            NeighbourTilesTracker tracker = GetComponent<NeighbourTilesTracker>();

            #region GENERATION VALUES USED IN THIS FUNCTION
            int width = generation.Width;
            #endregion

            for (int i = 0; i < generation.SmoothAmount; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < terrainGenerator.PerlinHeightList[x]; y++)
                    {
                        if (x == 0 || y == 0 || x == width - 1 || y == terrainGenerator.PerlinHeightList[x])
                        {
                            generation.Map[x, y] = 1;
                        }
                        else
                        {
                            int surroundingGroundCount = tracker.GetSurroundingGroundCount(x, y);
                            if (surroundingGroundCount > 4)
                            {
                                generation.Map[x, y] = 1;
                            }
                            else if (surroundingGroundCount < 4)
                            {
                                generation.Map[x, y] = 2;
                            }

                        }
                        if (x == terrainGenerator.ObstacleCoord.x && y == terrainGenerator.ObstacleCoord.y)
                        {
                            int obstaclePosX = terrainGenerator.ObstacleCoord.x;
                            int obstaclePosY = terrainGenerator.ObstacleCoord.y;
                            generation.Map[obstaclePosX, obstaclePosY] = -1;
                        }
                    }
                }
            }

        }

    }
}
