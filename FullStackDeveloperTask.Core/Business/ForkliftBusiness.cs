using FullStackDeveloperTask.Core.Parsers;
using FullStackDeveloperTask.Core.Readers;
using FullStackDeveloperTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Action = FullStackDeveloperTask.Data.Models.Action;

namespace FullStackDeveloperTask.Core.Business
{
    public class ForkliftBusiness(ForkliftFileReader forkliftFileReader, ICommandParser commandParser, IRoboticsContext context) : IForkliftBusiness
    {
        public async Task ImportAsync(Stream fileStream, string extension, CancellationToken cancellationToken = default)
        {
            var forklift = await forkliftFileReader.ReadForkliftDataAsync(fileStream, extension, cancellationToken);

            await context.Forklifts.AddRangeAsync(forklift, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Forklift[]> GetForkliftsAsync(int? skip, int? take, CancellationToken cancellationToken = default)
        {
            IQueryable<Forklift> forkliftsQuery = context.Forklifts.OrderBy(f => f.Name);

            if (skip is not null)
            {
                forkliftsQuery = forkliftsQuery.Skip(skip.Value);
            }

            if (take is not null)
            {
                forkliftsQuery = forkliftsQuery.Take(take.Value);
            }

            var forklifts = await forkliftsQuery.ToArrayAsync(cancellationToken);

            return forklifts;
        }

        public Command ExecuteCommand(string command)
        {
            var commandResult = new Command();
            try
            {
                var parsedCommand = commandParser.ParseCommand(command.ToUpperInvariant());

                var actions = parsedCommand
                .Select(t => new Action 
                {
                    Direcction = t.Key,
                    Value = t.Value
                })
                .ToList();

                commandResult.Actions = actions;
            }
            catch (FormatException e)
            {
                commandResult.Error = "Format error: " + e.Message;
            }
            catch (NotSupportedException e)
            {
                commandResult.Error = "Not supported action error: " + e.Message;
            }
            catch (Exception e)
            {
                commandResult.Error = "Command execution error: " + e.Message;
            }

            return commandResult;
        }

        private bool HasInvalidLetter(string cmd) => Regex.IsMatch(cmd, @"(?i)[A-Z-[FBLR]]");
    }
}
