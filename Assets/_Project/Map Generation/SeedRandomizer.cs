using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class SeedRandomizer : MonoBehaviour
    {
        private GenerationBehaviour generation;

        public void RandomizeSeed()
        {
            generation = GetComponent<GenerationBehaviour>();
            generation.Seed = Time.time;
        }
    }
}