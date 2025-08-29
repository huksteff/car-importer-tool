using System;
using System.IO;
using System.Linq;
using Editor.ImporterTool.Utilities.Dropdown;
using Editor.ImporterTool.Utilities.Extensions;
using Editor.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.ImportSettingsPage
{
    public class ImportSettingsController : IController
    {
        private readonly ImporterToolContext _context;
        private readonly ImportSettingsModel _model;
        private readonly ImportSettingsContainer _container;
        private readonly DropdownOptionsData DropdownOptionsData;
        private readonly ControllersList _controllersList = new();
        private string _shaderPath = "Assets/Content/Shaders/CarBaseShader.shadergraph";

        public ImportSettingsController(ImporterToolContext context, ImportSettingsModel model,
            ImportSettingsContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }

        public void Init()
        {
            _context.ImportBlockModel.OnAdd += ShowSettings;
            _context.CarSettingsModel.SaveModel += SaveSettings;
        }

        public void Dispose()
        {
            _context.ImportBlockModel.OnAdd -= ShowSettings;
            _context.CarSettingsModel.SaveModel -= SaveSettings;

            _controllersList.Dispose();
            _controllersList.Clear();
        }

        private void ShowSettings()
        {
            _container.CarName.value = _context.ImportBlockModel.CarPrefab.name;
            _container.ScaleFactor.value = _context.ImportBlockModel.CarImporter.globalScale;
            _container.ImportVisibilityToggle.value = _context.ImportBlockModel.CarImporter.importVisibility;

            _container.IsReadableToggle.value = _context.ImportBlockModel.CarImporter.isReadable;

            // Dropdown
            _container.IndexFormatDropdown.Dropdown.index = 0;

            SetMaterialsList(_context.ImportBlockModel.FbxPath);
            AddDropdowns();
        }

        private void AddDropdowns()
        {
            var meshCompressionDropdown = new DropdownModel("MeshCompression");
            var indexFormatDropdown = new DropdownModel("IndexFormat");
            var materialsListDropdown = new DropdownModel("SelectMaterial");

            meshCompressionDropdown.OnValueChanged += (value, index) => SetMeshCompression(value);
            indexFormatDropdown.OnValueChanged += (value, index) => SetIndexFormat(value);
            materialsListDropdown.OnValueChanged += (value, index) => SelectMaterial(value);

            _model.DropdownModelList.Add(meshCompressionDropdown);
            _model.DropdownModelList.Add(indexFormatDropdown);
            _model.DropdownModelList.Add(materialsListDropdown);

            _controllersList.Add(new DropdownController(meshCompressionDropdown, _container.MeshCompressionDropdown));
            _controllersList.Add(new DropdownController(indexFormatDropdown, _container.IndexFormatDropdown));
            _controllersList.Add(new DropdownController(materialsListDropdown, _container.SelectMaterialDropdown));

            _controllersList.Init();
        }

        private void SetMaterialsList(string assetPath)
        {
            Material[] modelMaterials = AssetDatabase.LoadAllAssetsAtPath(assetPath)
                .Where(mat => mat is Material)
                .Cast<Material>()
                .ToArray();

            DropdownOptionsData.MaterialsOption.Clear();
            _model.MaterialList.Clear();
            DropdownOptionsData.MaterialsOption.Add("None");
            _container.SelectMaterialDropdown.DefaultIndex = 0;

            foreach (var material in modelMaterials)
            {
                _model.MaterialList.Add(material.name, material);
                DropdownOptionsData.MaterialsOption.Add(material.name);
            }
        }

        private void SetIndexFormat(string value)
        {
            switch (value)
            {
                case ("Auto"):
                    _context.ImportBlockModel.CarImporter.indexFormat = ModelImporterIndexFormat.Auto;
                    break;
                case ("UInt16"):
                    _context.ImportBlockModel.CarImporter.indexFormat = ModelImporterIndexFormat.UInt16;
                    break;
                case ("UInt32"):
                    _context.ImportBlockModel.CarImporter.indexFormat = ModelImporterIndexFormat.UInt32;
                    break;
            }
        }

        private void SetMeshCompression(string value)
        {
            switch (value)
            {
                case ("Off"):
                    _context.ImportBlockModel.CarImporter.meshCompression = ModelImporterMeshCompression.Off;
                    break;
                case ("Low"):
                    _context.ImportBlockModel.CarImporter.meshCompression = ModelImporterMeshCompression.Low;
                    break;
                case ("Medium"):
                    _context.ImportBlockModel.CarImporter.meshCompression = ModelImporterMeshCompression.Medium;
                    break;
                case ("High"):
                    _context.ImportBlockModel.CarImporter.meshCompression = ModelImporterMeshCompression.High;
                    break;
            }
        }

        private void SelectMaterial(string value)
        {
            if (value == "None")
            {
                _container.MaterialPanelContainer.Clear();
                return;
            }

            ShowMaterialSettings(value);
        }

        private void ShowMaterialSettings(string value)
        {
            _container.MaterialPanelContainer.Clear();

            Shader targetShader = AssetDatabase.LoadAssetAtPath<Shader>(_shaderPath);
            Material targetMaterial = FindMaterialInFolder(value, targetShader);
            int propertyCount = targetShader.GetPropertyCount();

            for (int i = 0; i < propertyCount; i++)
            {
                string propertyName = targetShader.GetPropertyName(i);

                ShaderPropertyType propertyType = targetShader.GetPropertyType(i);

                string propertyDescription = targetShader.GetPropertyDescription(i);

                VisualElement field = null;

                switch (propertyType)
                {
                    case ShaderPropertyType.Color:
                        var colorField = new ColorField(propertyDescription);
                        colorField.value = targetMaterial.GetColor(propertyName);
                        colorField.AddStyle("color-field");
                        colorField.RegisterValueChangedCallback(evt =>
                            targetMaterial.SetColor(propertyName, evt.newValue));
                        field = colorField;
                        break;
                    case ShaderPropertyType.Float:
                        var floatField = new FloatField(propertyDescription);
                        floatField.value = targetMaterial.GetFloat(propertyName);
                        floatField.AddStyle("float-field");
                        floatField.RegisterValueChangedCallback(evt =>
                            targetMaterial.SetFloat(propertyName, evt.newValue));
                        field = floatField;
                        break;
                    case ShaderPropertyType.Texture:
                        var textureField = new ObjectField(propertyDescription);
                        textureField.objectType = typeof(Texture);
                        textureField.AddStyle("texture-field");
                        textureField.value = targetMaterial.GetTexture(propertyName);
                        textureField.RegisterValueChangedCallback(evt =>
                            targetMaterial.SetTexture(propertyName, (Texture)evt.newValue));
                        field = textureField;
                        break;
                }

                if (field != null) _container.MaterialPanelContainer.Add(field);
            }
        }

        private Material FindMaterialInFolder(string key, Shader targetShader)
        {
            if (!File.Exists(Path.Combine(_context.ImportBlockModel.currentFolderPath, key + ".mat")))
            {
                Material targetMaterial = new Material(targetShader);

                targetMaterial.name = key;

                string materialPath = Path.Combine(_context.ImportBlockModel.currentFolderPath,
                    targetMaterial.name + ".mat");

                AssetDatabase.CreateAsset(targetMaterial, materialPath);
                AssetDatabase.SaveAssets();
                _model.MaterialList[key] = targetMaterial;
                AssetDatabase.Refresh();
            }

            return _model.MaterialList[key];
        }

        private void SaveSettings()
        {
            var importer = _context.ImportBlockModel.CarImporter;

            if (importer.name != _container.CarName.value)
                RenameFile();

            if (importer != null)
            {
                importer.globalScale = _container.ScaleFactor.value;
                importer.bakeAxisConversion = _container.BakeAxisConversionToggle.value;
                importer.importVisibility = _container.ImportVisibilityToggle.value;
                importer.isReadable = _container.IsReadableToggle.value;
                importer.addCollider = _container.GenerateCollidersToggle.value;
                importer.keepQuads = _container.KeepQuadsToggle.value;
                importer.weldVertices = _container.WeldVrticesToggle.value;
                importer.generateSecondaryUV = _container.GenerateLightmapUVsToggle.value;

                UpdateMaterials(importer);

                try
                {
                    importer.SaveAndReimport();
                    _model.ImportModel();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to save importer settings: {e.Message}");
                }
            }
            else
            {
                Debug.LogError("CarImporter is null");
            }
            
            _context.CarSettingsModel.Reset();
        }

        private void UpdateMaterials(ModelImporter importer)
        {
            string assetPath = importer.assetPath;
    
            Material[] modelMaterials = AssetDatabase.LoadAllAssetsAtPath(assetPath)
                .Where(mat => mat is Material)
                .Cast<Material>()
                .ToArray();
            
            foreach (Material originalMaterial in modelMaterials)
            {
                if (originalMaterial != null)
                {
                    if (_model.MaterialList.TryGetValue(originalMaterial.name, out Material newMaterial) && newMaterial != null)
                    {
                        var materialIdentifier = new AssetImporter.SourceAssetIdentifier(typeof(Material), originalMaterial.name);
                
                        importer.RemoveRemap(materialIdentifier);
                        importer.AddRemap(materialIdentifier, newMaterial);
                    }
                }
            }
        }

        private void RenameFile()
        {
            string oldFolderPath = _context.ImportBlockModel.currentFolderPath;

            string[] assetsInFolder = AssetDatabase.FindAssets("", new[] { oldFolderPath });

            foreach (string guid in assetsInFolder)
            {
                string assetsPath = AssetDatabase.GUIDToAssetPath(guid);

                if (assetsPath.Contains(".fbx") || assetsPath.Contains(".prefab"))
                    AssetDatabase.RenameAsset(assetsPath, _container.CarName.value);
            }

            string parentDir = Path.GetDirectoryName(oldFolderPath);
            string newFolderPath = Path.Combine(parentDir, _container.CarName.value);
            string renameFolder = AssetDatabase.MoveAsset(oldFolderPath, newFolderPath);

            _context.ImportBlockModel.currentFolderPath = newFolderPath;

            AssetDatabase.Refresh();
        }

        private void PickObject()
        {
            EditorGUIUtility.PingObject(_context.ImportBlockModel.CarPrefab);
            Selection.activeObject = _context.ImportBlockModel.CarPrefab;
        }

        private void ShowExplorer()
        {
            EditorUtility.RevealInFinder(_context.ImportBlockModel.currentFolderPath);
        }
    }
}