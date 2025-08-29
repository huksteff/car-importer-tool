using System.Collections.Generic;
using Editor.Utilities;
using UnityEngine;

namespace Editor.ImporterTool.StatsPage
{
    public class StatsController : IController
    {
        private readonly ImporterToolContext _context;
        private readonly StatsContainer _container;

        public StatsController(ImporterToolContext context, StatsContainer container)
        {
            _context = context;
            _container = container;
        }
        
        public void Init()
        {
            GetVerticesStats();
        }

        private void GetVerticesStats()
        {
            HashSet<Vector3> uniqueVerticesSet = new HashSet<Vector3>();
            List<Mesh> meshes = new List<Mesh>();
            int uniqueVerts = 0;
            int summaryVerts = 0;
            int triangles = 0;
            
            foreach (Transform child in _context.ImportBlockModel.CarPrefab.GetComponentsInChildren<Transform>(true))
            {
                MeshFilter meshFilter = child.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.sharedMesh != null)
                {
                    Mesh mesh = meshFilter.sharedMesh;
                    foreach (Vector3 vertex in mesh.vertices)
                    {
                        uniqueVerticesSet.Add(vertex);
                    }
                    meshes.Add(meshFilter.sharedMesh);
                    
                    triangles += mesh.triangles.Length / 3;
                }

                SkinnedMeshRenderer skinnedMeshRenderer = child.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
                {
                    Mesh mesh = skinnedMeshRenderer.sharedMesh;
                    foreach (Vector3 vertex in mesh.vertices)
                    {
                        uniqueVerticesSet.Add(vertex);
                    }
                    meshes.Add(skinnedMeshRenderer.sharedMesh);

                    triangles += mesh.triangles.Length / 3;
                }
            }

            uniqueVerts = uniqueVerticesSet.Count;
            
            foreach (Mesh mesh in meshes)
            {
                 summaryVerts += mesh.vertexCount;
                 
                _container.Stat.text = 
                    $"Model Name: {_context.ImportBlockModel.CarPrefab.name}\n" +
                    $"Vertices: {summaryVerts}\n" +
                    $"Unique Vertices: {uniqueVerts}\n" +
                    $"Triangles: {triangles}\n" +
                    $"isReadable: {mesh.isReadable}\n" +
                    $"Index format: {mesh.indexFormat}";   
            }
        }

        public void Dispose()
        {
            
        }
    }
}