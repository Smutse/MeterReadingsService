using Microsoft.AspNetCore.Mvc;

namespace EnsekMeterReadingsService.Controllers
{
    /// <summary>
    /// Get meter readings currently only consumed by swagger
    /// </summary>
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
    }
}
