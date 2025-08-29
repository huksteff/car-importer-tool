using System.Collections.Generic;

namespace Editor.ImporterTool.Utilities.Dropdown
{
    public class DropdownOptionsBaseData
    {
        public List<string> List;

        public void Add(string item)
        {
            List.Add(item);
        }

        public void Remove(string item)
        {
            List.Remove(item);
        }

        public void Clear()
        {
            List.Clear();
        }
    }
}