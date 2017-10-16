using System;

namespace EA.Core.Models
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}