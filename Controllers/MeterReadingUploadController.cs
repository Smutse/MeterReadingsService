using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnsekMeterReadingsService.Dto;
using EnsekMeterReadingsService.Interfaces;

namespace EnsekMeterReadingsService.Controllers
{
    /// <summary>
    /// Get meter readings currently only consumed by swagger
    /// </summary>
    [ApiController]
    public class MeterReadingUploadController : Controller
    {
        private readonly IMeterReadingUploadRepository _meterReadingUploadRepository;
        private readonly IMapper _mapper;
        private readonly iAccountRepository _accountRepository;
        public MeterReadingUploadController(IMeterReadingUploadRepository meterReadingUploadRepository, IMapper mapper, iAccountRepository accountRepository)
        {
            _meterReadingUploadRepository = meterReadingUploadRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        [Route("get-meter-readings")]
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MeterReadingUpload>))]
        public IActionResult GetMeterReadingUploads()
        {
            var meterReadingUploads = _mapper.Map<List<MeterReadingUploadDto>>(_meterReadingUploadRepository.GetMeterReadingUploads());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(meterReadingUploads);
            }
        }

        /// <summary>
        /// Entry point and key funtionality for uploading csv data
        /// </summary>
        [Route("meter-reading-uploads")]

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(205)]
        public IActionResult AddListMeterReadings([FromForm] CSVUpload csvUpload)
        {
            //Get Classes 
            HelperClass helper = new HelperClass();
            ReturnValues returnValues = new ReturnValues();

            List<MeterReadingUpload> meterReadingsToUpload = new List<MeterReadingUpload>();
            var accountIdList = _mapper.Map<List<AccountDto>>(_accountRepository.GetAccounts());


            returnValues.SuccessfulRecordsCount = 0;
            returnValues.FailedRecordsCount = 0;
            var existingMeterReadings = _mapper.Map<List<MeterReadingUploadDto>>(_meterReadingUploadRepository.GetMeterReadingUploads());

            using (var reader = new StreamReader(csvUpload.ReadingsCSV.OpenReadStream()))
            {
                var csvString = reader.ReadToEndAsync();
                if (csvString != null)
                {
                    string[] csvRecordArray = csvString.ToString().Split('\n');

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
                            meterReadingUpload = helper.ParseMeterReading(accountIdList, cells, existingMeterReadings);

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
            }
            try
            {
                if (_meterReadingUploadRepository.CreateMeterReadingUploads(meterReadingsToUpload))
                {
                    return Ok(returnValues);
                }
                else
                {
                    return StatusCode(500, "Internal server error");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(returnValues);
        }
    }
    
}
