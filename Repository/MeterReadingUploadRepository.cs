using AutoMapper;
using EnsekMeterReadingsService.Dto;
using EnsekMeterReadingsService.Interfaces;

namespace EnsekMeterReadingsService.Repository
{
    public class MeterReadingUploadRepository : IMeterReadingUploadRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MeterReadingUploadRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<MeterReadingUpload> GetMeterReadingUploads()
        {
            return _context.MeterReadingUploads.OrderBy(p => p.Id).ToList();
        }

        public bool CreateMeterReadingUploads(List<MeterReadingUpload> meterReadingUploads)
        {
            try
            {
                var meterReadingUploadsMap = _mapper.Map<List<MeterReadingUpload>>(meterReadingUploads);
                _context.MeterReadingUploads.AddRange(meterReadingUploadsMap);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
