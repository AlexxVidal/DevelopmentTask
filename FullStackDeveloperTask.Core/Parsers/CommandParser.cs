using System.Text.RegularExpressions;

namespace FullStackDeveloperTask.Core.Parsers
{
    public class CommandParser : ICommandParser
    {
        public List<KeyValuePair<char, int>> ParseCommand(string command)
        {
            if (!char.IsLetter(command[0]))
            {
                throw new FormatException("The command format is not correct. Must start with an action [F,B,R,L]");
            }

            if (HasInvalidLetter(command))
            {
                throw new FormatException("The command format is not correct. Invalid character/action spotted");
            }

            var steps = new List<KeyValuePair<char, int>>();
            int i = 0;
            while (i < command.Length)
            {
                char action = char.ToUpper(command[i]);
                if (action is not ('F' or 'B' or 'R' or 'L'))
                {
                    throw new FormatException("Invalid action");
                }

                int start = i + 1;
                int end = start;
                while (end < command.Length && char.IsDigit(command[end]))
                {
                    end++;
                }

                if (end == start)
                {
                    throw new FormatException("The action must be followed by a number");
                }

                if (!int.TryParse(command.Substring(start, end - start), out int value))
                {
                    throw new FormatException("Invalid number for the action: " + action);
                }

                if (action is 'R' || action is 'L')
                {
                    if (value < 0 || value > 360 || value % 90 != 0)
                    {
                        throw new NotSupportedException("The degrees mus be: 0, 90, 180, 270 or 360");
                    }
                }

                steps.Add(new KeyValuePair<char, int>(action, value));

                i = end;
            }

            return steps;
        }

        private bool HasInvalidLetter(string cmd) => Regex.IsMatch(cmd, @"(?i)[A-Z-[FBLR]]");
    }
}
