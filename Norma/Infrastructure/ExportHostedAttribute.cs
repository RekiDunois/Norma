using System;

namespace Norma.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExportHostedAttribute:Attribute
    {
        public bool SingleInstance { get; set; } = true;
        public bool LazyCreate { get; set; } = true;
    }
}
