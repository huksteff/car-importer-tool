using System;
using Editor.ImporterTool.Utilities.Dropdown;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Editor.ImporterTool.ModelSettings
{
    public class CarSettingsModel
    {
        public Action BackToImport;
        public Action SaveModel;

        public void Reset()
        {
            BackToImport?.Invoke();
        }

        public void Import()
        {
            SaveModel?.Invoke();
        }
    }
}