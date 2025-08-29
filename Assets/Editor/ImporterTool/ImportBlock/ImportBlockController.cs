using System;
using System.IO;
using System.Linq;
using Editor.ImporterTool.Utilities;
using Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Editor.ImporterTool.ImportBlock
{
    public class ImportBlockController : IController
    {
        private readonly ImporterToolContext _context;
        private readonly ImportBlockModel _model;
        private readonly ImportBlockContainer _container;

        public ImportBlockController(ImporterToolContext context, ImportBlockModel model, ImportBlockContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }
 
        public void Init()
        {
            _container.Manipulator.OnAddFile += ImportFile;
            _context.CarSettingsModel.BackToImport += GoToImport;
        }

        public void Dispose()
        {
            _container.Manipulator.OnAddFile -= ImportFile;
            _model.currentFolderPath = "";
            _context.CarSettingsModel.BackToImport -= GoToImport;
        }

        private void GoToImport()
        {
            _container.ShowElement(_container.ImportField); 
            _model.Reset();
        }

        private void ImportFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
    
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            string newFolderPath = GetUniqueFolderPath(ImporterToolConsts.RootFolder, fileName);

            if (!CreateFolder(newFolderPath)) return;
            
            _model.currentFolderPath = newFolderPath;
        
            string destinationFilePath = Path.Combine(newFolderPath, Path.GetFileName(filePath));
            CreateFile(filePath, destinationFilePath);
        
            CreatePrefab(destinationFilePath, newFolderPath);
        }

        private string GetUniqueFolderPath(string parentFolder, string folderName)
        {
            string basePath = Path.Combine(parentFolder, folderName);
            string uniquePath = basePath;
    
            var counter = 0;
            while (AssetDatabase.IsValidFolder(uniquePath))
            {
                counter++;
                uniquePath = $"{basePath} {counter}";
            }
    
            return uniquePath;
        }

        private bool CreateFolder(string fullFolderPath)
        {
            try
            {
                string parentFolder = Path.GetDirectoryName(fullFolderPath);
                string folderName = Path.GetFileName(fullFolderPath);
        
                if (AssetDatabase.IsValidFolder(fullFolderPath))
                {
                    Debug.LogWarning($"Folder already exists: {fullFolderPath}");
                    return false;
                }
        
                string guid = AssetDatabase.CreateFolder(parentFolder, folderName);
                AssetDatabase.Refresh();
        
                return !string.IsNullOrEmpty(guid);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to create folder: {e.Message}");
                return false;
            }
        }

        private void CreateFile(string sourcePath, string destinationPath)
        {
            try
            {
                if (File.Exists(sourcePath))
                {
                    FileUtil.CopyFileOrDirectory(sourcePath, destinationPath);
                }
                else if (Directory.Exists(sourcePath))
                {
                    FileUtil.CopyFileOrDirectory(sourcePath, destinationPath);
                }
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to copy file: {e.Message}");
            }
        }

        private void CreatePrefab(string modelPath, string targetFolderPath)
        {
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(modelPath);
            if (model == null)
            {
                Debug.LogError($"Failed to load model at path: {modelPath}");
                return;
            }
            
            GameObject parentObject = new GameObject($"{model.name}_Car");
            GameObject rootObject = new GameObject("_Root");
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(model) as GameObject;
            
            rootObject.transform.SetParent(parentObject.transform);
            rootObject.transform.localPosition = Vector3.zero;
            rootObject.transform.localRotation = Quaternion.identity;
            rootObject.transform.localScale = Vector3.one;

            if (prefabInstance != null)
            {
                prefabInstance.transform.SetParent(rootObject.transform);
                prefabInstance.transform.localPosition = Vector3.zero;
                prefabInstance.transform.localRotation = Quaternion.identity;
                prefabInstance.transform.localScale = Vector3.one;
            }

            string prefabPath = Path.Combine(targetFolderPath, $"{model.name}.prefab");
            string fbxPath = Path.Combine(targetFolderPath, $"{model.name}.fbx");
    
            var carPrefab = PrefabUtility.SaveAsPrefabAsset(parentObject, prefabPath, out bool success);
            GameObject.DestroyImmediate(parentObject);
            
            // Debug.Log(_context.CarSettingsModel.CarPrefab.name);
            
            if (!success)
            {
                Debug.LogError($"Failed to save prefab at: {prefabPath}");
                return;
            }

            AssetDatabase.Refresh();
            
            // Debug.Log(_model.currentFolderPath);
            
            StartImportParams(modelPath);
            
            _container.HideElement(_container.ImportField);
            
            _model.Add(carPrefab, rootObject, prefabInstance, fbxPath);
        }

        private void StartImportParams(string assetPath)
        {
            _model.CarImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
            
            _model.CarImporter.globalScale = 1;
            _model.CarImporter.isReadable = false;
            _model.CarImporter.importVisibility = true;
            _model.CarImporter.importCameras = false;
            _model.CarImporter.importAnimation = false;
            _model.CarImporter.importLights = false;
            _model.CarImporter.importConstraints = false;
            _model.CarImporter.importBlendShapes = false;

            _model.CarImporter.SaveAndReimport();
        }
        
    }
}