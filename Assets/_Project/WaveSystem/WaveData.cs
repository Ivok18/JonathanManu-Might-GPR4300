using System;

namespace Might.WaveSystem
{
    [Serializable]
    public class WaveData
    {
        public string WaveName;
        public WaveState waveState;
        public int totalEnemies;
        public EnemySpawnerData spawner;
    }
}
