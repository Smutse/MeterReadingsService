namespace EnsekMeterReadingsService.Interfaces
{
    public interface IMeterReadingUploadRepository
    {
        ICollection<MeterReadingUpload> GetMeterReadingUploads();
        Boolean CreateMeterReadingUploads(List<MeterReadingUpload> meterReadingUpload);   
    }
}
