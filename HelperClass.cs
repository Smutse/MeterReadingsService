using System.Globalization;
using System.Text.RegularExpressions;

namespace EnsekMeterReadingsService
{
    public class HelperClass
    {
        public MeterReadingUpload ParseMeterReading(List<string> accountIdList, string[] cells, DataContext dataContext, List<MeterReadingUpload> existingMeterReadings)
        {
            
            MeterReadingUpload meterReadingUpload = new MeterReadingUpload();
            if (accountIdList.Contains(cells[0]))
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

        public bool CheckForDuplicatesAndNewerReadings(List<MeterReadingUpload> existingMeterReadings, string[] cells, DateTime dt)
        {
            bool duplicateFound = false;
            foreach (MeterReadingUpload existingReading in existingMeterReadings)
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
