using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.MapGeneration
{
    public class SeedRandomizer : MonoBehaviour
    {
        private GenerationBehaviour generation;

        private void Awake()
        {
            generation = GetComponent<GenerationBehaviour>();

        }

        public void RandomizeSeed()
        {
            generation.Seed = Time.time;
        }
    }
}