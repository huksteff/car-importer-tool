using System;
using System.Collections.Generic;
using Editor.ImporterTool.Utilities.Dropdown;
using UnityEngine;

namespace Editor.ImporterTool.ComponentsPage
{
    public class ComponentPageModel
    {
        public readonly List<DropdownModel> DropdownModelList = new ();
        public readonly Dictionary<string, Component> Components = new();
        public string SelectedComponentKey;
        
        public DropdownModel AddModel(string id)
        {
            var newModel = new DropdownModel(id);
            
            DropdownModelList.Add(newModel);
            
            return newModel;
        }

        public void AddNewComponent(string id, Component component)
        {
            Components.Add(id, component);
        }

        public void RemoveComponent(string id)
        {
            Components.Remove(id);
        }

        public Component GetComponent(string id)
        {
            Component component = Components[id];
            
            return component;
        }
    }
}