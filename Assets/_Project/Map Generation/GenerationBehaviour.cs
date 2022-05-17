using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace Might.MapGeneration
{
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
        [SerializeField] private bool generationHasEnded;

        [Header("Tile")]
        [SerializeField] private TileBase groundTile;
        [SerializeField] private TileBase caveTile;
        [SerializeField] private TileBase obstacleTile;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap caveTilemap;
        [SerializeField] private Tilemap obstacleTilemap;

        [Header("Entity Spawner")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;


        public delegate void GenerationEndedCallback();
        public static event GenerationEndedCallback OnGenerationEndedCallback;
        
        public int[,] Map { get; set; }

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

        public Tilemap ObstacleTilemap
        {
            get => obstacleTilemap;
            set => obstacleTilemap = value;
        }
 
        
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
            #endregion

            #region Process Map
            MapProcessor mapProcessor = GetComponent<MapProcessor>();
            mapProcessor.ProcessMap(Map, 5);
            #endregion

            #region Render Map
            MapRenderer mapRenderer = GetComponent<MapRenderer>();
            mapRenderer.RenderMap(Map, GroundTilemap, CaveTilemap, ObstacleTilemap,
                groundTile, caveTile, obstacleTile);
            #endregion

            OnGenerationEndedCallback?.Invoke();

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
        
    }
}