using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norma.Config
{
    public class BotConfiguration
    {
        public string BotToken { get; init; }
        public string HostAddress { get; init; }
        public IEnumerable<string> UserIds { get; init; }
    }
}
