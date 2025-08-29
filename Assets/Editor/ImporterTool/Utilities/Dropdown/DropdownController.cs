using System;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.Utilities.Dropdown
{
    public class DropdownController : IController
    {
        private readonly DropdownModel _model;
        private readonly DropdownContainer _container;

        public DropdownController(DropdownModel model, DropdownContainer container)
        {
            _model = model;
            _container = container;
        }
        
        public void Init()
        {
            if (_model.CurrentIndex >= 0 && _model.CurrentIndex < _container.Dropdown.choices.Count)
            {
                _container.Dropdown.index = _model.CurrentIndex;
            }
            
            _container.Dropdown.RegisterValueChangedCallback(ChangeValue);
        }

        public void Dispose()
        {
            _container.Dropdown.UnregisterValueChangedCallback(ChangeValue);
        }

        private void ChangeValue(ChangeEvent<string> evt)
        {
            _model.ChangeValue(evt.newValue, _container.Dropdown.index);
        }
    }
}