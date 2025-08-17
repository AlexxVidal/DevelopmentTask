using FullStackDeveloperTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Readers
{
    public class ForkliftFileReader(CsvForkliftReader csvReader, JsonForkliftReader jsonReader)
    {

        public async Task<IEnumerable<Forklift>> ReadForkliftDataAsync(Stream stream, string extension, CancellationToken cancellationToken = default)
        {
            switch (extension)
            {
                case ".csv":
                    return await csvReader.ReadForkliftDataAsync(stream);
                case ".json":
                    return await jsonReader.ReadForkliftDataAsync(stream, cancellationToken);
                default:
                    throw new NotSupportedException("File type not supported");
            }
        }
    }
}
