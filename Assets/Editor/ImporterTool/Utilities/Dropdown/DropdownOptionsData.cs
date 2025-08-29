using System.Collections.Generic;
using UnityEngine;

namespace Editor.ImporterTool.Utilities.Dropdown
{
    public class DropdownOptionsData : DropdownOptionsBaseData
    {
        public static List<string> MeshCompressionOption = new() { "Off", "Low", "Medium", "High" };
        public static List<string> IndexFormatOption = new() { "Auto", "UInt16", "UInt32" };
        public static List<string> NormalsOption = new() { "Import", "Calculate", "None" };
        public static List<string> NormalsModeOption = new()
            { "Area And Angle Weighted", "Area Weighted", "Angle Weighted", "Unweighted" };
        public static List<string> MaterialsOption = new();
        public static List<string> ComponentsOption = new() {"None", "Rigidbody", "Box Collider", "CarScript"};
    }
}