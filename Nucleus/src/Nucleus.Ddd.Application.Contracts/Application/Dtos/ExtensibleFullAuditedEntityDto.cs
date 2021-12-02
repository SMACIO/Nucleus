﻿using System;
using Nucleus.Auditing;

namespace Nucleus.Application.Dtos
{
    /// <summary>
    /// This class can be inherited by DTO classes to implement <see cref="IFullAuditedObject"/> interface.
    /// It also implements the <see cref="IHasExtraProperties"/> interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    [Serializable]
    public abstract class ExtensibleFullAuditedEntityDto<TPrimaryKey> : ExtensibleAuditedEntityDto<TPrimaryKey>, IFullAuditedObject
    {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// This class can be inherited by DTO classes to implement <see cref="IFullAuditedObject"/> interface.
    /// It also implements the <see cref="IHasExtraProperties"/> interface.
    /// </summary>
    [Serializable]
    public abstract class ExtensibleFullAuditedEntityDto : ExtensibleAuditedEntityDto, IFullAuditedObject
    {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTime? DeletionTime { get; set; }
    }
}

