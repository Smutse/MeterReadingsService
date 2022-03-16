using AutoMapper;
using EnsekMeterReadingsService.Dto;

namespace EnsekMeterReadingsService.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<MeterReadingUpload, MeterReadingUploadDto>();
        }
    }
}
