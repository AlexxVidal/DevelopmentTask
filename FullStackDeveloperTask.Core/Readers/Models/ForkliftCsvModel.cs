using CsvHelper.Configuration.Attributes;

namespace FullStackDeveloperTask.Core.Readers.Models
{
    public class ForkliftCsvModel
    {
        [Name("name")]
        public string Name { get; set; }

        [Name("model_number")]
        public string ModelNumber { get; set; }

        [Name("manufacturing_date")]
        public DateTime ManufacturingDate { get; set; }
    }
}
