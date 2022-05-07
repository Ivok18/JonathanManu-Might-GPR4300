using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class NeighbourTilesTracker : MonoBehaviour
    {
        private GenerationBehaviour generation;

        public int GetSurroundingGroundCount(int gridX, int gridY)
        {
            generation = GetComponent<GenerationBehaviour>();

            #region GENERATION VALUES USED IN THIS FUNCTION  
            int width = generation.Width;
            int height = generation.Height;   
            #endregion 

            int groundCount = 0;
            for (int nebX = gridX - 1; nebX <= gridX + 1; nebX++)
            {
                for (int nebY = gridY - 1; nebY <= gridY + 1; nebY++)
                {
                    if (nebX >= 0 && nebX < width && nebY >= 0 && nebY < height)
                    {
                        if (nebX != gridX || nebY != gridY)
                        {
                            if (generation.Map[nebX, nebY] == 1)
                            {
                                groundCount++;
                            }
                        }
                    }
                }
            }
            return groundCount;

        }
        public int GetSurroundingCaveCount(int gridX, int gridY)
        {
            generation = GetComponent<GenerationBehaviour>();

            #region GENERATION VALUES USED IN THIS FUNCTION  
            int width = generation.Width;
            int height = generation.Height;
            #endregion

            int caveCount = 0;
            for (int nebX = gridX - 1; nebX <= gridX + 1; nebX++)
            {
                for (int nebY = gridY - 1; nebY <= gridY + 1; nebY++)
                {
                    if (nebX >= 0 && nebX < width && nebY >= 0 && nebY < height)
                    {
                        if (nebX != gridX || nebY != gridY)
                        {
                            if (generation.Map[nebX, nebY] == 2)
                            {
                                caveCount++;
                            }
                        }
                    }
                }
            }
            return caveCount;

        }

    }

}
