using System;
using System.Collections.Generic;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.Utilities.Dropdown
{
    public class DropdownContainer
    {
        public readonly DropdownField Dropdown;
        public int DefaultIndex;

        public DropdownContainer(VisualElement root, string label, List<string> options, string tooltip = null, string style = null)
        {
            Dropdown = new DropdownField(label, options, DefaultIndex);
            Dropdown.tooltip = tooltip;
            
            Dropdown.AddToClassList(style);
            root.Add(Dropdown);
        }
    }
}