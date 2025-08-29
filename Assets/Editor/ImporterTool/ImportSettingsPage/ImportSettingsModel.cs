using System;
using System.Collections.Generic;
using Editor.ImporterTool.Utilities.Dropdown;
using UnityEngine;

namespace Editor.ImporterTool.ImportSettingsPage
{
    public class ImportSettingsModel
    {
        public event Action ToImport; 
        public readonly List<DropdownModel> DropdownModelList = new ();
        public Dictionary<string, Material> MaterialList = new();
        
        public DropdownModel AddModel(string id)
        {
            var newModel = new DropdownModel(id);
            
            DropdownModelList.Add(newModel);
            
            return newModel;
        }

        public void ImportModel()
        {
            ToImport?.Invoke();
        }
    }
}