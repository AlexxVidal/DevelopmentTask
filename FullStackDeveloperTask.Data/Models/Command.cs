using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Data.Models
{
    public class Command
    {
        public List<Action>? Actions { get; set; } = null;

        public string? Error { get; set; } = null;
    }
}
