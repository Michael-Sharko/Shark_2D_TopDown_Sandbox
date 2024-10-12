using UnityEngine;

namespace Shark.WorldGeneration
{
    [CreateAssetMenu(fileName = "New World Settings", menuName = "World Settings", order = 51)]
    public class WorldSettings : ScriptableObject
    {
        [Header("Island Settings")]
        public int width = 100;
        public int height = 100;
        public float scale = 0.1f;
        public float islandRadius = 40f;
        public Gradient terrainGradient;
        
        [Range(0, 1)] 
        public float falloffStrength = 0.5f;
    }
}
