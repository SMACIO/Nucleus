using System;
using Nucleus.ObjectExtending;

namespace Nucleus.Application.Dtos
{
    [Serializable]
    public abstract class ExtensibleEntityDto<TKey> : ExtensibleObject, IEntityDto<TKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TKey Id { get; set; }

        public override string ToString()
        {
            return $"[DTO: {GetType().Name}] Id = {Id}";
        }
    }

    [Serializable]
    public abstract class ExtensibleEntityDto : ExtensibleObject, IEntityDto
    {
        public override string ToString()
        {
            return $"[DTO: {GetType().Name}]";
        }
    }
}

