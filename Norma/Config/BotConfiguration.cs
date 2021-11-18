using System.Collections.Generic;

namespace Norma.Config
{
    public class BotConfiguration
    {
        public string BotToken { get; init; }
        public string HostAddress { get; init; }
        public IEnumerable<string> UserIds { get; init; }
    }
}
