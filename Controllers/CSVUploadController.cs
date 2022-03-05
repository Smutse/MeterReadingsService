using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace EnsekMeterReadingsService.Controllers
{
    [Route("meter-reading-uploads")]
    [ApiController]
    public class CSVUploadController : Controller
    {
        private readonly DataContext dataContext;
        public CSVUploadController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpPost]
        public async Task<ActionResult<ReturnValues>> AddListMeterReadings([FromForm] CSVUpload csvUpload)
        {
            //Get Classes 
            HelperClass helper = new HelperClass();
            ReturnValues returnValues = new ReturnValues();
            AccountController accountController = new AccountController();

            List<MeterReadingUpload> meterReadingsToUpload = new List<MeterReadingUpload>();
            List<string> accountIdList = await accountController.GetAccountsAsync(dataContext);
            List<MeterReadingUpload> existingMeterReadings = new List<MeterReadingUpload>();


            returnValues.SuccessfulRecordsCount = 0;
            returnValues.FailedRecordsCount = 0;
            existingMeterReadings = dataContext.GetReadingUploads(dataContext);

            using (var reader = new StreamReader(csvUpload.ReadingsCSV.OpenReadStream()))
            {
                var csvString = await reader.ReadToEndAsync();
                string[] csvRecordArray = csvString.Split('\n');

                foreach (var row in csvRecordArray)
                {
                    if (!string.IsNullOrWhiteSpace(row))
                    {
                        //Considered parsing by row header name but from past experience
                        //row header names are often mispelled and it seems more common
                        //practice to go by row order, ordinarily the client would be asked
                        //their preference or there would be an option
                        string[] cells = row.Split(',');

                        MeterReadingUpload meterReadingUpload = new MeterReadingUpload();
                        meterReadingUpload = helper.ParseMeterReading(accountIdList, cells, dataContext, existingMeterReadings);

                        if (meterReadingUpload.AccountId > 0)
                        {
                            meterReadingsToUpload.Add(meterReadingUpload);
                            returnValues.SuccessfulRecordsCount++;
                        }
                        else
                        {
                            returnValues.FailedRecordsCount++;
                        }
                    }
                }
            }
            try
            {
                dataContext.MeterReadingUploads.AddRange(meterReadingsToUpload);
                dataContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(returnValues);
        }
    }
}
