using UnityEngine;
using Might.MapGeneration;
using System.Collections;

namespace Might.Pathfinding
{
    public class PathfindingScanDelayer : MonoBehaviour
    {       
        private void OnEnable()
        {
            GenerationBehaviour.OnGenerationEndedCallback += ScanMap;
        }

        public void ScanMap()
        {
            StartCoroutine("DelayScan", 0.01f);
        }

        private void OnDisable()
        {
            GenerationBehaviour.OnGenerationEndedCallback -= ScanMap;
        }

        public IEnumerator DelayScan(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            AstarPath.active.Scan();
        }
       
    }
}
