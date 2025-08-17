using System.Runtime.Serialization;

namespace FullStackDeveloperTask.Api.Models
{
    public class GetForkliftResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ModelNumber { get; set; }

        [DataMember]
        public DateTime ManufacturingDate { get; set; }
    }
}
