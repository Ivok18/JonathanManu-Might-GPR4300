using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralGenLearning.Basic
{
    public class ProeceduralGeneration : MonoBehaviour
    {
        [SerializeField] private int mapWidth;
        [SerializeField] private int mapHeight;
        [SerializeField] private int minStoneDepth;
        [SerializeField] private int maxStoneDepth;

        [SerializeField] private Tilemap dirtTilemap;
        [SerializeField] private Tilemap grassTilemap;        
        [SerializeField] private Tilemap stoneTilemap;
        [SerializeField] private Tile dirtTile;
        [SerializeField] private Tile grassTile;
        [SerializeField] private Tile stoneTile;

        [Range(0,100)] [SerializeField] private float heightValue;
        [Range(0,100)] [SerializeField] private float smoothness;
        [SerializeField] private float seed;

        private void Start()
        {
            Generate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Generate();
            }
        }

        private void Clear()
        {
            dirtTilemap.ClearAllTiles();
            grassTilemap.ClearAllTiles();
            stoneTilemap.ClearAllTiles();
        }

       public void Generate()
        {
            Clear();
            RandomizeSeed();
            RandomizeSmoothness();
            RandomizeHeightAmplifier();

            for (int x = 0; x < mapWidth; x++)
            {
                mapHeight = Mathf.RoundToInt(heightValue * Mathf.PerlinNoise(x / smoothness, seed));

                int minStoneLastYPos = mapHeight - minStoneDepth;
                int maxStoneLastYpos = mapHeight - maxStoneDepth;
                int stoneLastYpos = Random.Range(minStoneLastYPos, maxStoneLastYpos);

                for (int y = 0; y < mapHeight; y++)
                {
                    if (y <= stoneLastYpos)
                    {
                        stoneTilemap.SetTile(new Vector3Int(x, y, 0), stoneTile);
                    }
                    else
                    {
                        dirtTilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
                    }
                }

                grassTilemap.SetTile(new Vector3Int(x, mapHeight, 0), grassTile);
            }
        }

        private void RandomizeHeightAmplifier()
        {
            heightValue = Random.Range(30, 50);
        }

        private void RandomizeSmoothness()
        {
            smoothness = Random.Range(1, 100);
        }

        private void RandomizeSeed()
        {
            seed = Random.Range(-1000000, 1000000);
        }
    }

}
