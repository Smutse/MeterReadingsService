namespace EnsekMeterReadingsService
{
    /// <summary>
    /// Values to return in rest request for inserting csv records 
    /// (could be made more generic)
    /// </summary>
    public class ReturnValues
    {
        public int SuccessfulRecordsCount { get; set; }
        public int FailedRecordsCount { get; set; }
    }
}
