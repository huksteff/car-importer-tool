using System;
using Editor.ImporterTool.ComponentsPage;
using Editor.ImporterTool.ImportBlock;
using Editor.ImporterTool.ImportSettingsPage;
using Editor.ImporterTool.StatsPage;
using Editor.ImporterTool.Utilities.Dropdown;
using Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Editor.ImporterTool.ModelSettings
{
    public class CarSettingsController : IController
    {
        private readonly ImporterToolContext _context;
        private readonly CarSettingsModel _model;
        private readonly CarSettingsContainer _container;
        private readonly DropdownOptionsData DropdownOptionsData;
        private readonly ControllersList _controllersList = new();

        public CarSettingsController(ImporterToolContext context, CarSettingsModel model, CarSettingsContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }
        
        public void Init()
        {
            _container.HideElement(_container.MenuContainer);
            _container.HideElement(_container.MainContainer);
            _container.HideElement(_container.ButtonsHorizContainer);

            _container.ImportSettingsContainer.Show();
            _container.ComponentPageContainer.Hide();
            _container.StatsContainer.Hide();
            
            _controllersList.Init();

            OpenImportSettingsPage();
            
            _context.ImportBlockModel.OnAdd += OnAddModel;

            _container.ImportSettingsButton.enabledSelf = false;
            
            _container.ImportSettingsButton.clicked += OpenImportSettingsPage;
            _container.ComponentEditorButton.clicked += OpenComponent;
            _container.StatsButton.clicked += OpenStats;
            
            _container.ApplyButton.clicked += SaveSettings;
            _container.PickObjectButton.clicked += PickObject;
            _container.ShowExplorerButton.clicked += ShowExplorer;

            _context.ImportSettingsModel.ToImport += OnBack;
        }

        public void Dispose()
        {
            _context.ImportBlockModel.OnAdd -= OnAddModel;
            
            _container.ImportSettingsButton.clicked -= OpenImportSettingsPage;
            _container.ComponentEditorButton.clicked -= OpenComponent;
            _container.StatsButton.clicked -= OpenStats;
            
            _controllersList.Dispose();
            _controllersList.Clear();
        }

        private void OpenImportSettingsPage()
        {
            HideAllPages();
            
            _controllersList.Add(new ImportSettingsController(_context, _context.ImportSettingsModel, _container.ImportSettingsContainer));
            _controllersList.Init();
            _container.ImportSettingsContainer.Show();
            _container.ShowElement(_container.ButtonsHorizContainer);
            _container.ImportSettingsButton.enabledSelf = false;
        }

        private void OpenComponent()
        {
            HideAllPages();
            
            _controllersList.Add(new ComponentPageController(_context, _container.ComponentPageContainer, _context.ComponentPageModel));
            _controllersList.Init();
            _container.ComponentPageContainer.Show();
            _container.ShowElement(_container.ButtonsHorizContainer);
            _container.ComponentEditorButton.enabledSelf = false;
        }

        private void OpenStats()
        {
            HideAllPages();
            
            _controllersList.Add(new StatsController(_context, _container.StatsContainer));
            _controllersList.Init();
            _container.StatsContainer.Show();
            _container.StatsButton.enabledSelf = false;
        }

        private void OnAddModel()
        {
            _container.ShowElement(_container.MenuContainer);
            _container.ShowElement(_container.MainContainer);
            _container.BackButton.clicked += OnBack;
        }

        private void OnBack()
        {
            _container.HideElement(_container.MenuContainer);
            _container.HideElement(_container.MainContainer);
            _container.ImportSettingsContainer.Show();
            _container.ComponentPageContainer.Hide();
            _container.StatsContainer.Hide();
            _container.ImportSettingsButton.enabledSelf = false;
            _container.ComponentEditorButton.enabledSelf = true;
            _container.StatsButton.enabledSelf = true;
            _model.Reset();
        }

        private void HideAllPages()
        {
            _container.ImportSettingsContainer.Hide();
            _container.ComponentPageContainer.Hide();
            _container.StatsContainer.Hide();
            _container.ImportSettingsButton.enabledSelf = true;
            _container.ComponentEditorButton.enabledSelf = true;
            _container.StatsButton.enabledSelf = true;
            _container.HideElement(_container.ButtonsHorizContainer);
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

        private void SaveSettings()
        {
            _model.Import();
        }
    }
}