using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norma.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExportHostedAttribute:Attribute
    {
        public bool SingleInstance { get; set; } = true;
        public bool LazyCreate { get; set; } = true;
    }
}
