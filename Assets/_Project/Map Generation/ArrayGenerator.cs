using UnityEngine;

namespace Might.MapGeneration
{
    public class ArrayGenerator : MonoBehaviour
    {
        public int[,] GenerateArray(int width, int height, bool empty)
        {         
            int[,] map = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (empty)
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
    }
}
