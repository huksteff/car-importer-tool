using System;

namespace Editor.ImporterTool.Utilities.Dropdown
{
    public class DropdownModel
    {
        public event Action<string, int> OnValueChanged;
        public string Id { get; }
        public int CurrentIndex { get; private set; }

        public DropdownModel(string id)
        {
            Id = id;
        }
        
        public void ChangeValue(string value, int index)
        {
            CurrentIndex = index;
            OnValueChanged?.Invoke(value, index);
        }
    }
}