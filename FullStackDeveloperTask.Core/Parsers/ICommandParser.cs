using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Parsers
{
    public interface ICommandParser
    {
        List<KeyValuePair<char, int>> ParseCommand(string command);
    }
}
