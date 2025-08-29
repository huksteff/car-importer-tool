using System.Collections.Generic;

namespace Editor.Utilities
{
    public class ControllersList : IController
    {
        private readonly List<IController> _controllers = new ();
        
        public void Add(IController controller)
        {
            _controllers.Add(controller);
        }

        public void Remove(IController controller)
        {
            _controllers.Remove(controller);
        }

        public void Clear()
        {
            _controllers.Clear();
        }
        
        public void Init()
        {
            foreach (var controller in _controllers)
            {
                controller.Init();
            }
            
        }

        public void Dispose()
        {
            foreach (var controller in _controllers)
            {
                controller.Dispose();
            }
        }
    }
}