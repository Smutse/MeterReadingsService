using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsekMeterReadingsService
{
    public class MeterReadingUpload
    {
        [Key]
        public int Id { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
        //Foreign key
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
