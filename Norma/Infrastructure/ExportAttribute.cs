﻿using System;

namespace Norma.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ExportAttribute : Attribute
    {
        public Type? ContractType { get; }
        public bool SingleInstance { get; set; } = true;
        public bool LazyCreate { get; set; } = true;

        public ExportAttribute() { }
        public ExportAttribute(Type contractType) => ContractType = contractType;
    }
}
