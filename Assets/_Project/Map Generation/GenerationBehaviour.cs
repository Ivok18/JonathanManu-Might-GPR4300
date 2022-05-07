using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace Might.MapGeneration
{
  
    public class Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public class GenerationBehaviour : MonoBehaviour
    {
        [Header("Terrain Generation")]
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float seed;
        [Range(0, 100)]
        [SerializeField] private float smoothness;


        [Header("Cave Generation")]
        [Range(0, 100)]
        [SerializeField] private int randomFillPercent;
        [SerializeField] private int smoothAmount;
        [SerializeField] private int caveMinimumSize;
        int[] perlinHeightList;

        [Header("Tile")]
        [SerializeField] private TileBase groundTile;
        [SerializeField] private TileBase caveTile;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap caveTilemap;

        [Header("Entity Spawner")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;

        public int Width
        {
            get => width;
            set => width = value;
        }
        public int Height
        {
            get => height;
            set => height = value;
        }
        public float Seed
        {
            get => seed;
            set => seed = value;
        }
        public float Smoothness
        {
            get => smoothness;
            set => smoothness = value;
        }
        public int RandomFillPercent
        {
            get => randomFillPercent;
            set => randomFillPercent = value;
        }
        public int SmoothAmount
        {
            get => smoothAmount;
            set => smoothAmount = value;
        }
        public int CaveMinimumSize
        {
            get => caveMinimumSize;
            set => caveMinimumSize = value;

        }
        public Tilemap GroundTilemap
        {
            get => groundTilemap;
            set => groundTilemap = value;
        }
        public Tilemap CaveTilemap
        {
            get => caveTilemap;
            set => caveTilemap = value;
        }
        public int[,] Map { get; set; }


        void Start()
        {
            Generation();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {            
                Generation();
            }
        }

        public void Generation()
        {
            #region Clear map tiles
            MapClearer mapClearer = GetComponent<MapClearer>();
            mapClearer.ClearMap();
            #endregion
            #region Clear entities
            ClearPlayer();
            ClearEnemy();
            #endregion
            #region Randomize seed
            SeedRandomizer seedRandomizer = GetComponent<SeedRandomizer>();
            seedRandomizer.RandomizeSeed();
            #endregion
            #region Generate map array
            ArrayGenerator arrayGenerator = GetComponent<ArrayGenerator>();
            Map = arrayGenerator.GenerateArray(Width, Height, true);
            #endregion
            #region Generate terrain
            TerrainGenerator terrainGenerator = GetComponent<TerrainGenerator>();
            Map = terrainGenerator.GenerateTerrain(Map);
            #endregion
            #region Smooth map
            MapSmoother mapSmoother = GetComponent<MapSmoother>();
            mapSmoother.SmoothMap();
            //SmoothMap();
            #endregion
            #region Process Map
            //ProcessMap(5);
            MapProcessor mapProcessor = GetComponent<MapProcessor>();
            mapProcessor.ProcessMap(Map, 5);
            #endregion
            #region Render Map
            MapRenderer mapRenderer = GetComponent<MapRenderer>();
            mapRenderer.RenderMap(Map, GroundTilemap, CaveTilemap, groundTile, caveTile);
            #endregion


            SpawnPlayer();
            SpawnEnemy();         
        }        

       

        public int GetSurroundingGroundCount(int gridX, int gridY)
        {
            int groundCount = 0;
            for (int nebX = gridX - 1; nebX <= gridX+1; nebX++)
            {
                for (int nebY = gridY - 1; nebY <= gridY + 1; nebY++)
                {
                    if(nebX >= 0 && nebX < Width && nebY >= 0 && nebY < Height)
                    {
                        if(nebX != gridX || nebY != gridY)
                        {
                            if (Map[nebX,nebY] == 1)
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
            int caveCount = 0;
            for (int nebX = gridX - 1; nebX <= gridX + 1; nebX++)
            {
                for (int nebY = gridY - 1; nebY <= gridY + 1; nebY++)
                {
                    if (nebX >= 0 && nebX < width && nebY >= 0 && nebY < height)
                    {
                        if (nebX != gridX || nebY != gridY)
                        {
                            if (Map[nebX, nebY] == 2)
                            {
                                caveCount++;
                            }
                        }
                    }
                }
            }
            return caveCount;

        }



        public void ClearPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                DestroyImmediate(player);
            }
        }
        public void ClearEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                DestroyImmediate(enemy);
            }
        }
        public void SpawnPlayer()
        {
            MapProcessor mapProcessor = GetComponent<MapProcessor>();

            foreach (List<Coord> region in mapProcessor.listOfRegions)
            {
                if (region.Count > caveMinimumSize)
                {
                    foreach (Coord tile in region)
                    { 
                        if (Map[tile.x, tile.y] == 2)
                        {
                            int surroundingCaveCount = GetSurroundingCaveCount(tile.x, tile.y);
                            if (surroundingCaveCount >= 8)
                            {
                                GameObject player = Instantiate(playerPrefab, new Vector3(tile.x +1, tile.y, 0), Quaternion.identity);
                                return;
                            }
                         
                        }
                        
                    }
                }
            }
           
        }
        public void SpawnEnemy()
        {
            MapProcessor mapProcessor = GetComponent<MapProcessor>();
            for (int x = width - 1; x >= 0 ; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    if(Map[x,y] == 2)
                    {
                        List<Coord> region = mapProcessor.GetRegionTiles(Map,x, y); ;
                        int caveCount = GetSurroundingCaveCount(x, y);
                        if(caveCount >= 8)
                        {
                            //Debug.Log("found cave tile");
                            Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
                            return;
                        }
                        //72 -> width 40-> height 39->fill percent 100 -> cave minomum size
                    }
                }
            }
            /*
            foreach (List<Coord> region in listOfRegions)
            {
                if (region.Count > caveMinimumSize)
                {
                    foreach (Coord tile in region)
                    {
                        if (Map[tile.x, tile.y] == 2)
                        {
                            if(tile.x > width/1.3f && tile.y == height /2)
                            {
                                int surroundingCaveCount = GetSurroundingCaveCount(tile.x, tile.y);
                                if (surroundingCaveCount >= 8)
                                {
                                    
                                    GameObject enemy = Instantiate(enemyPrefab, new Vector3(tile.x + 5, tile.y, 0), Quaternion.identity);
                                    return;
                                }

                            }

                        }

                    }
                }
            }*/
        }
    }
}