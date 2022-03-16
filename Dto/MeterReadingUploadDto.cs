namespace EnsekMeterReadingsService.Dto
{
    public class MeterReadingUploadDto
    {
        public int Id { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
        //Foreign key
        public int AccountId { get; set; }
    }
}
