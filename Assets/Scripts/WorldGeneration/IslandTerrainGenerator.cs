using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Shark.WorldGeneration
{
    public class IslandTerrainGenerator : MonoBehaviour
    {
        [SerializeField]
        bool _autoUpdate;

        public WorldSettings worldSettings;
        public bool worldSettingsFoldout;

        [SerializeField, HideInInspector]
        MeshFilter _meshFilter;
        [SerializeField, HideInInspector]
        MeshRenderer _meshRenderer;

        private void Start()
        {
            Initialize();
            GenerateIsland();
        }

        public void Initialize()
        {
            if (_meshFilter == null)
            {
                _meshFilter = gameObject.AddComponent<MeshFilter>();
                _meshRenderer = gameObject.AddComponent<MeshRenderer>();
                
                _meshRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                _meshFilter.sharedMesh = new Mesh();

                ConstructMesh();
                GenerateAndApplyTexture();
            }
        }

        private void ConstructMesh()
        {
            Mesh mesh = new();
            Vector3[] vertices = new Vector3[4]
            {
                new(0, 0),
                new(0, worldSettings.height),
                new(worldSettings.width, 0),
                new(worldSettings.width, worldSettings.height)
            };
            int[] triangles = new int[6]
            {
                0, 1, 2,
                2, 1, 3
            };
            Vector2[] uvs = new Vector2[4]
            {
                new(0, 0),
                new(0, 1),
                new(1, 0),
                new(1, 1)
            };

            mesh.Clear();
            mesh.name = "Island";

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            _meshFilter.sharedMesh = mesh;
        }

        private void GenerateAndApplyTexture()
        {
            Texture2D texture = GenerateIslandTexture();
            texture.filterMode = FilterMode.Point;
            texture.Apply();

            _meshRenderer.sharedMaterial.SetTexture("_BaseMap", texture);
        }

        public void GenerateIsland()
        {
            Initialize();
            GenerateAndApplyTexture();
        }

        public void OnIslandTerrainUpdated()
        {
            if (_autoUpdate)
            {
                GenerateIsland();
            }
        }

        private Texture2D GenerateIslandTexture()
        {
            Texture2D texture = new(worldSettings.width, worldSettings.height);
            for (int x = 0; x < worldSettings.width; x++)
            {
                for (int y = 0; y < worldSettings.height; y++)
                {
                    float biomeValue = Mathf.PerlinNoise(x * worldSettings.scale, y * worldSettings.scale);
                    float distanceFromCenter = CalculateDistanceFromCenter(x, y);
                    float falloff = ApplyFalloff(distanceFromCenter);

                    biomeValue *= 1 - falloff;

                    Color tileColor = worldSettings.terrainGradient.Evaluate(biomeValue);
                    texture.SetPixel(x, y, tileColor);
                }
            }
            return texture;
        }

        private float CalculateDistanceFromCenter(int x, int y)
        {
            Vector2 center = new(worldSettings.width / 2, worldSettings.height / 2);
            return Vector2.Distance(new(x, y), center) / worldSettings.islandRadius;
        }

        private float ApplyFalloff(float distance)
        {
            return Mathf.Clamp01(Mathf.Pow(distance, worldSettings.falloffStrength));
        }
    }
}