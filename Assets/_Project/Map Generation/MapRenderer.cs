using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Might.MapGeneration
{
    public class MapRenderer : MonoBehaviour
    {
        private GenerationBehaviour generation;
        private TerrainGenerator terrainGenerator;

        public void RenderMap(int[,] map, Tilemap groundTilemap, Tilemap caveTilemap, Tilemap ObstacleTilemap, 
            TileBase groundTilebase, TileBase caveTilebase, TileBase obstacleTilebase)
        {
            generation = GetComponent<GenerationBehaviour>();
            terrainGenerator = GetComponent<TerrainGenerator>();

            #region GENERATION VALUES USED IN THIS FUNCTION
            int width = generation.Width;
            int height = generation.Height;
            #endregion

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 1 || map[x, y] == 0)
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                    }
                    else if (map[x, y] == 2)
                    {
                        caveTilemap.SetTile(new Vector3Int(x, y, 0), caveTilebase);
                    }
                    else if (map[x,y] == -1)
                    {
                        ObstacleTilemap.SetTile(new Vector3Int(x, y, 0), obstacleTilebase);
                    }
                }
            }
        }
    }
}
