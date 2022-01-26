using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Norma.Config
{
    public class ProgramConfiguration
    {
        public IEnumerable<string> AssemblyNames { get; init; }
        public IEnumerable<Assembly> Assemblies
        {
            //use Assembly.Load as a selector to load the assembly which the name in AssemblyNames
            get => AssemblyNames.Select(Assembly.Load);
        }
    }
}
