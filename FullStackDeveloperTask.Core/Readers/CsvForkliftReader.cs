using CsvHelper;
using CsvHelper.Configuration;
using FullStackDeveloperTask.Core.Readers.Models;
using FullStackDeveloperTask.Core.Readers.Models.Extensions;
using FullStackDeveloperTask.Data.Models;
using System.Globalization;

namespace FullStackDeveloperTask.Core.Readers
{
    public class CsvForkliftReader : IForkliftFileReader
    {
        private readonly CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        public Task<IEnumerable<Forklift>> ReadForkliftDataAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream is null)
                throw new ArgumentNullException("No data to read", nameof(stream));

            ForkliftCsvModel[] records;
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, configuration))
            {
                records = csv.GetRecords<ForkliftCsvModel>().ToArray();
            }

            var forklifts = records.Select(f => f.ToForklift());

            return Task.FromResult(forklifts);
        }
    }
}
