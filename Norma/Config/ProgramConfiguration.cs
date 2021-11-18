using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Norma.Config
{
    public class ProgramConfiguration
    {
        public IEnumerable<string> AssemblyNames { get; init; }
        public IEnumerable<Assembly> Assemblies { get => AssemblyNames.Select(Assembly.Load); }
    }
}
