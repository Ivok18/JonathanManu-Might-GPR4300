using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace ProceduralGenLearning.Advanced
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

    public class ProceduralGen : MonoBehaviour
    {
        [Header("Terrain Generation")]
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float seed;
        [Range(0, 100)]
        [SerializeField] private float smoothness;


        [Header("Cave Generation")]
        [Range(0, 100)]
        //[SerializeField] float modifier;
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


        List<List<Coord>> listOfRegions;

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
            ClearMap();
            ClearPlayer();
            ClearEnemy();
            RandomizeSeed();
            InitPerlinHeightList();
            Map = GenerateArray(width, height, true);
            Map = TerrainGeneration(Map);
            SmoothMap();
            ProcessMap(5);
            RenderMap(Map, groundTilemap, caveTilemap, groundTile, caveTile);
            SpawnPlayer();
            SpawnEnemy();
            
            
        }

        public void InitPerlinHeightList()
        {
            perlinHeightList = new int[width];
        }

        public int[,] GenerateArray(int width, int height, bool empty)
        {
            int[,] map = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(empty)
                    {
                        map[x, y] = 0;                  
                    }
                    else
                    {
                        map[x, y] = 1;
                    }
                }
            }

            return map;
        }

        public int[,] TerrainGeneration(int[,] map)
        {
            System.Random pseudoRandom = new System.Random(seed.GetHashCode());
            int perlinHeight;
            for (int x = 0; x < width; x++)
            {
                perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height/2);
                perlinHeight += height / 2;
                perlinHeightList[x] = perlinHeight;
                for (int y = 0; y < perlinHeightList[x]; y++)
                {
                    //int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
                    //map[x, y] = caveValue;
                    /*if(caveValue == 1)
                    {
                        map[x, y] = 2;
                    }
                    else if(caveValue == 0)
                    {
                        map[x, y] = 1;
                    }*/
                    if(pseudoRandom.Next(1, 100) < randomFillPercent)
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

        public int[,] TerrainGeneration2(int[,] map)
        {
            int perlinHeight;

            //1st
            for (int x = 0; x < width; x++)
            {
                perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height / 2);
                perlinHeight += height / 2;
                perlinHeightList[x] = perlinHeight;

             /*   int minYstart = 0;
                int maxYStart = 2;
                int yStart = UnityEngine.Random.Range(minYstart, maxYStart); */

                for (int y = 0; y < height ; y++)
                {
                    
                    int minCaveLastYPos = perlinHeight - 1;
                    int maxCaveLastYpos = perlinHeight + 2;
                    var caveLastYpos = UnityEngine.Random.Range(minCaveLastYPos, maxCaveLastYpos);

                    if (y <= caveLastYpos)
                    {
                        map[x, y] = 2;
                    }
                    else
                    {
                        map[x, y] = 1;
                    }
                                    
                }

            }

            for (int x = 0; x < width; x++)
            {
                /*   int minYstart = 0;
                   int maxYStart = 2;
                   int yStart = UnityEngine.Random.Range(minYstart, maxYStart); */

                for (int y = perlinHeightList[x]; y >= 0; y--)
                {

                    int minCaveLastYPos = height - perlinHeightList[x] - 1;
                    int maxCaveLastYpos = height - perlinHeightList[x] + 2;
                    var caveLastYpos = UnityEngine.Random.Range(minCaveLastYPos, maxCaveLastYpos);

                    if (y >= caveLastYpos)
                    {
                        map[x, y] = 2;
                    }
                    else
                    {
                        map[x, y] = 1;
                    }

                }


            }



            return map;
        }

        public void RenderMap(int [,] map, Tilemap groundTilemap, Tilemap caveTilemap, TileBase groundTilebase, TileBase caveTilebase)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(map[x,y] == 1 || map[x,y] == 0)
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                    }
                    else if(map[x, y] == 2)
                    {
                        caveTilemap.SetTile(new Vector3Int(x, y, 0), caveTilebase);
                    }
                }
            }
        }
        
        public void ClearMap()
        {
            groundTilemap.ClearAllTiles();
            caveTilemap.ClearAllTiles();
        }

        public void ClearPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
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

        public void SmoothMap()
        {
            for (int i = 0; i < smoothAmount; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < perlinHeightList[x]; y++)
                    {
                        if (x == 0 || y == 0 || x == width - 1 || y == perlinHeightList[x] - 1)
                        {
                            Map[x, y] = 1;
                        }
                        else
                        {
                            int surroundingGroundCount = GetSurroundingGroundCount(x, y);
                            if (surroundingGroundCount > 4)
                            {
                                Map[x, y] = 1;
                            }
                            else if (surroundingGroundCount < 4)
                            {
                                Map[x, y] = 2;
                            }
                           
                        }
                    }
                }
            }
            
        }

        public int GetSurroundingGroundCount(int gridX, int gridY)
        {
            int groundCount = 0;
            for (int nebX = gridX - 1; nebX <= gridX+1; nebX++)
            {
                for (int nebY = gridY - 1; nebY <= gridY + 1; nebY++)
                {
                    if(nebX >= 0 && nebX < width && nebY >= 0 && nebY < height)
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

        public void RandomizeSeed()
        {
            seed = Time.time;
        }

        public void ProcessMap(int processAmount)
        {
            for (int i = 0; i < processAmount; i++)
            {
                List<List<Coord>> caveRegions = GetRegions(2);

                int minimumSize = caveMinimumSize;
                foreach (List<Coord> region in caveRegions)
                {
                    if (region.Count < minimumSize)
                    {
                        foreach (Coord tile in region)
                        {
                            Map[tile.x, tile.y] = 1;
                        }
                    }

                }
            }
            



           

      





        }
        public List<List<Coord>> GetRegions(int tileType)
        {
            List<List<Coord>> regions = new List<List<Coord>>();
            int[,] mapFlags = new int[width, height];
           

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    
                    if(mapFlags[x,y] == 0 && Map[x,y] == tileType)
                    {
                        List<Coord> newRegion = GetRegionTiles(x, y);
                        regions.Add(newRegion);
                        //Debug.Log(regions.Count);
                        //Debug.Log(newRegion.Count);

                        foreach(Coord tile in newRegion)
                        {
                            mapFlags[tile.x, tile.y] = 1;
                            //Instantiate(enemyPrefab, new Vector3(tile.x, tile.y, 0), Quaternion.identity);
                        }
                    }
                   
                }
            }

         

            listOfRegions = regions;
            return listOfRegions;

            
        }

        public List<Coord> GetRegionTiles(int startX, int startY)
        {
            List<Coord> tiles = new List<Coord>();
            int[,] mapFlags = new int[width, height];
            int tileType = Map[startX, startY];

            Queue<Coord> queue = new Queue<Coord>();
            queue.Enqueue(new Coord(startX, startY));
            mapFlags[startX, startY] = 1;

            while(queue.Count > 0)
            {
                Coord tile = queue.Dequeue();
                tiles.Add(tile);

                for (int x = tile.x - 1; x <= tile.x + 1; x++)
                {
                    for (int y = 0; y <= tile.y + 1; y++)
                    {
                        if (IsInMapRange(x, y) && (x == tile.x || y == tile.y))
                        {
                            if (mapFlags[x, y] == 0 && Map[x, y] == tileType)
                            {
                                mapFlags[x, y] = 1;
                                queue.Enqueue(new Coord(x, y));
                            }
                        }
                    }
                }
            }

            //Debug.Log(tiles.Count);
            
            return tiles;
        }

        public bool IsInMapRange(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return true;
            }

            return false;
        }

        public void SpawnPlayer()
        {
            foreach (List<Coord> region in listOfRegions)
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
            for (int x = width - 1; x >= 0 ; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    if(Map[x,y] == 2)
                    {
                        List<Coord> region = GetRegionTiles(x, y);
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