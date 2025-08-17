using FullStackDeveloperTask.Core.Readers.Models;
using FullStackDeveloperTask.Core.Readers.Models.Extensions;
using FullStackDeveloperTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Readers
{
    public class JsonForkliftReader : IForkliftFileReader
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        public async Task<IEnumerable<Forklift>> ReadForkliftDataAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream is null)
                throw new ArgumentNullException("No data to read", nameof(stream));

            var records = await JsonSerializer.DeserializeAsync<List<ForkliftJsonModel>>(stream, Options, cancellationToken) ?? [];

            var formlifts = records.Select(f => f.ToForklift());

            return formlifts;
        }
    }
}
