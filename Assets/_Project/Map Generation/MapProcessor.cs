using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class MapProcessor : MonoBehaviour
    {
        private GenerationBehaviour generation;

        public List<List<Coord>> ListOfRegions { get; set; }

        public void ProcessMap(int [,] targetMap, int processAmount)
        {
            generation = GetComponent<GenerationBehaviour>();

            #region GENERATION VALUES USED IN THIS FUNCTION
            int caveMinimumSize = generation.CaveMinimumSize;
            #endregion

            for (int i = 0; i < processAmount; i++)
            {
                List<List<Coord>> caveRegions = GetRegions(targetMap, 2);

                foreach (List<Coord> region in caveRegions)
                {
                    if (region.Count < caveMinimumSize)
                    {
                        foreach (Coord tile in region)
                        {
                            targetMap[tile.x, tile.y] = 1;
                        }
                    }

                }
           
            }
        }

        public List<List<Coord>> GetRegions(int[,] targetMap, int tileType)
        {
            generation = GetComponent<GenerationBehaviour>();

            #region GENERATION VALUES USED IN THIS FUNCTION
            int width = generation.Width;
            int height = generation.Height;
            #endregion

            List<List<Coord>> regions = new List<List<Coord>>();
            int[,] mapFlags = new int[width, height];


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    if (mapFlags[x, y] == 0 && targetMap[x, y] == tileType)
                    {
                        List<Coord> newRegion = GetRegionTiles(targetMap, x, y);
                        regions.Add(newRegion);
                        
                        foreach (Coord tile in newRegion)
                        {
                            mapFlags[tile.x, tile.y] = 1;
                        }
                    }

                }
            }



            ListOfRegions = regions;
            return ListOfRegions;
        }

        public List<Coord> GetRegionTiles(int[,] map, int startX, int startY)
        {
            #region GENERATION VALUES USED IN THIS FUNCTION
            int width = generation.Width;
            int height = generation.Height;
            #endregion

            List<Coord> tiles = new List<Coord>();
            int[,] mapFlags = new int[width, height];
            int tileType = map[startX, startY];

            Queue<Coord> queue = new Queue<Coord>();
            queue.Enqueue(new Coord(startX, startY));
            mapFlags[startX, startY] = 1;

            while (queue.Count > 0)
            {
                Coord tile = queue.Dequeue();
                tiles.Add(tile);

                for (int x = tile.x - 1; x <= tile.x + 1; x++)
                {
                    for (int y = 0; y <= tile.y + 1; y++)
                    {
                        if (IsInMapRange(x, y) && (x == tile.x || y == tile.y))
                        {
                            if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                            {
                                mapFlags[x, y] = 1;
                                queue.Enqueue(new Coord(x, y));
                            }
                        }
                    }
                }
            }

            return tiles;
        }

        public bool IsInMapRange(int x, int y)
        {
            #region GENERATION VALUES USED IN THIS FUNCTION
            int width = generation.Width;
            int height = generation.Height;
            #endregion

            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return true;
            }

            return false;
        }


      
    }
}
