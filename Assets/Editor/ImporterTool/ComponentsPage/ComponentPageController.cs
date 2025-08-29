using System;
using Editor.ImporterTool.Utilities.Dropdown;
using Editor.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Editor.ImporterTool.ComponentsPage
{
    public class ComponentPageController : IController
    {
        private readonly ImporterToolContext _context;
        private readonly ComponentPageContainer _container;
        private readonly ComponentPageModel _model;
        private readonly ControllersList _controllersList = new();
        private readonly DropdownOptionsData DropdownOptionsData;

        public ComponentPageController(ImporterToolContext context, ComponentPageContainer container,
            ComponentPageModel model)
        {
            _context = context;
            _container = container;
            _model = model;
        }

        public void Init()
        {
            _container.HideElement(_container.RemoveComponentsButton);

            var scriptsDropdownModel = new DropdownModel("DropdownModel");

            scriptsDropdownModel.OnValueChanged += (value, index) => AddScript(value);

            _model.DropdownModelList.Add(scriptsDropdownModel);

            _controllersList.Add(new DropdownController(scriptsDropdownModel, _container.ScriptDropdown));

            _controllersList.Init();

            _container.RemoveComponentsButton.clicked += RemoveComponents;
        }

        public void Dispose()
        {
            _controllersList.Clear();
            _controllersList.Dispose();
            _model.DropdownModelList.Clear();
            _container.RemoveComponentsButton.clicked -= RemoveComponents;
        }

        private void AddScript(string value)
        {
            Debug.Log($"Change value to -> {value}");

            if (_context.ImportBlockModel.CarPrefab == null)
            {
                Debug.LogError("CarPrefab is null");
                return;
            }

            _model.SelectedComponentKey = value;

            switch (value)
            {
                case "Rigidbody":
                    if (!HasComponent<Rigidbody>())
                        AddOrGetComponent<Rigidbody>(value);
                    _container.ShowElement(_container.RemoveComponentsButton);
                    ComponentSettings(value);
                    break;
                case "Box Collider":
                    if (!HasComponent<BoxCollider>())
                        AddOrGetComponent<BoxCollider>(value);
                    _container.ShowElement(_container.RemoveComponentsButton);
                    ComponentSettings(value);
                    break;
                case "CarScript":
                    if (!HasComponent<FirstScript>())
                        AddOrGetComponent<FirstScript>(value);
                    _container.ShowElement(_container.RemoveComponentsButton);
                    ComponentSettings(value);
                    break;
                case "None":
                    _container.HideElement(_container.RemoveComponentsButton);
                    break;
            }
        }

        private bool HasComponent<T>() where T : Component
        {
            return _context.ImportBlockModel.CarPrefab.GetComponent<T>() != null;
        }

        private void AddOrGetComponent<T>(string value) where T : Component
        {
            _model.AddNewComponent(value, _context.ImportBlockModel.CarPrefab.AddComponent<T>());
            ComponentSettings(value);
        }

        private void RemoveComponents()
        {
            _container.ConfigContainer.Clear();

            UnityEngine.Object.DestroyImmediate(_model.GetComponent(_model.SelectedComponentKey), true);
            _model.RemoveComponent(_model.SelectedComponentKey);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            _container.ScriptDropdown.Dropdown.index = 0;
        }

        private void ComponentSettings(string value)
        {
            _container.ConfigContainer.Clear();

            if (_model.Components.TryGetValue(value, out Component component))
            {
                SerializedObject serializedComponent = new SerializedObject(component);
                SerializedProperty iterator = serializedComponent.GetIterator();

                iterator.Next(true);

                while (iterator.NextVisible(false))
                {
                    if (iterator.name == "m_Script") continue;

                    var propertyField = new PropertyField(iterator);
                    _container.ConfigContainer.Add(propertyField);
                }

                _container.ConfigContainer.Bind(serializedComponent);
            }
        }
    }
}