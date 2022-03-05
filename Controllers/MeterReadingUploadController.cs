using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekMeterReadingsService.Controllers
{
    [Route("get-meter-readings")]
    [ApiController]
    public class MeterReadingUploadController : Controller
    {
        private readonly DataContext dataContext;
        public MeterReadingUploadController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<MeterReadingUpload>>> GetReadingUploads()
        {
            return await this.dataContext.MeterReadingUploads.ToListAsync();
        }
        
        [HttpPost]
        public async Task<ActionResult<List<MeterReadingUpload>>> AddListMeterReadings(List<MeterReadingUpload> meterReadingUploads)
        {
            return Ok(meterReadingUploads);
        }        
    }
}
