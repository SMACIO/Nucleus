using System;
using Nucleus.Reflection;
using Nucleus.Threading;

namespace Nucleus.Http.Modeling
{
    [Serializable]
    public class ReturnValueApiDescriptionModel
    {
        public string Type { get; set; }

        public string TypeSimple { get; set; }

        public ReturnValueApiDescriptionModel()
        {

        }

        public static ReturnValueApiDescriptionModel Create(Type type)
        {
            var unwrappedType = AsyncHelper.UnwrapTask(type);

            return new ReturnValueApiDescriptionModel
            {
                Type = TypeHelper.GetFullNameHandlingNullableAndGenerics(unwrappedType),
                TypeSimple = ApiTypeNameHelper.GetSimpleTypeName(unwrappedType)
            };
        }
    }
}


