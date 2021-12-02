using System;

namespace Nucleus.Http.Modeling
{
    [Serializable]
    public class ControllerInterfaceApiDescriptionModel
    {
        public string Type { get; set; }

        public ControllerInterfaceApiDescriptionModel()
        {

        }

        public static ControllerInterfaceApiDescriptionModel Create(Type type)
        {
            return new ControllerInterfaceApiDescriptionModel
            {
                Type = type.FullName
            };
        }
    }
}

