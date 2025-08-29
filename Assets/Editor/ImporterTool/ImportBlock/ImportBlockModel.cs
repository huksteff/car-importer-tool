using System;
using UnityEditor;
using UnityEngine;

namespace Editor.ImporterTool.ImportBlock
{
    public class ImportBlockModel
    {
        public Action OnAdd;
        
        public string currentFolderPath = "";
        public GameObject CarPrefab;
        public GameObject CarRoot;
        public GameObject CarObject;
        public ModelImporter CarImporter;
        public string FbxPath;
        
        public void Add(GameObject carPrefab, GameObject rootObject, GameObject parentObject, string fbxPath)
        {
            CarPrefab = carPrefab;
            CarObject = rootObject;
            CarRoot = parentObject;

            FbxPath = fbxPath.Replace("\\", "/");
            
            currentFolderPath = currentFolderPath.Replace("\\", "/");
            
            OnAdd?.Invoke();
        }

        public void Reset()
        {
            CarPrefab = null;
            CarObject = null;
            CarRoot = null;
            CarImporter = null;
            
            currentFolderPath = "";
        }
    }
}