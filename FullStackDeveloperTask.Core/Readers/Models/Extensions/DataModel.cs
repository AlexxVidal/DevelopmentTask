using FullStackDeveloperTask.Data.Models;

namespace FullStackDeveloperTask.Core.Readers.Models.Extensions
{
    public static class DataModel
    {
        public static Forklift ToForklift(this ForkliftCsvModel forkliftCsvModel)
        {
            return new Forklift
            {
                ManufacturingDate = forkliftCsvModel.ManufacturingDate,
                ModelNumber = forkliftCsvModel.ModelNumber,
                Name = forkliftCsvModel.Name
            };
        }

        public static Forklift ToForklift(this ForkliftJsonModel forkliftJsonModel)
        {
            return new Forklift
            {
                ManufacturingDate = forkliftJsonModel.ManufacturingDate,
                ModelNumber = forkliftJsonModel.ModelNumber,
                Name = forkliftJsonModel.Name
            };
        }
    }
}
