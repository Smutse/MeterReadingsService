using System.Globalization;
using System.Text.RegularExpressions;
using EnsekMeterReadingsService.Dto;

namespace EnsekMeterReadingsService
{
    public class HelperClass
    {
        /// <summary>
        /// Parse Meter Readings
        /// </summary>
        /// <param name="accountIdList"></param>
        /// <param name="cells"></param>
        /// <param name="dataContext"></param>
        /// <param name="existingMeterReadings"></param>
        /// <returns></returns>
        public MeterReadingUpload ParseMeterReading(List<AccountDto> accountIdList, string[] cells, List<MeterReadingUploadDto> existingMeterReadings)
        {
            
            MeterReadingUpload meterReadingUpload = new MeterReadingUpload();
            if (accountIdList.Count > 0)
            {
                try
                {
                    DateTime dt = new DateTime();

                    if (DateTime.TryParseExact(cells[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    {
                        //Should we allow negative meter readings?
                        //Assumption is no as the format is NNNNN but clarification would be preferable.
                        if (Regex.IsMatch(cells[2], @"\b\d{5}\b") && Regex.IsMatch(cells[2], @"^[0-9]"))
                        {
                            if (!CheckForDuplicatesAndNewerReadings(existingMeterReadings, cells, dt))
                            {
                                meterReadingUpload.AccountId = int.Parse(cells[0]);
                                meterReadingUpload.MeterReadingDateTime = dt;
                                meterReadingUpload.MeterReadValue = cells[2];
                            }
                        }
                    }
                }
                catch
                {
                    return meterReadingUpload;
                }
            }
            return meterReadingUpload;
        }
        /// <summary>
        /// Check for duplicates in lists of meter readings
        /// </summary>
        /// <param name="existingMeterReadings"></param>
        /// <param name="cells"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CheckForDuplicatesAndNewerReadings(List<MeterReadingUploadDto> existingMeterReadings, string[] cells, DateTime dt)
        {
            bool duplicateFound = false;
            foreach (MeterReadingUploadDto existingReading in existingMeterReadings)
            {
                if (existingReading.AccountId == int.Parse(cells[0]) && existingReading.MeterReadValue == cells[2] && existingReading.MeterReadingDateTime >= dt)
                {
                    duplicateFound = true;
                }
            }
            return duplicateFound;
        }
    }
}
