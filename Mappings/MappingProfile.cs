using AutoMapper;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserResponseDto>();

            // Surat mappings
            CreateMap<Surat, SuratResponseDto>()
                .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedBy.Username))
                .ForMember(dest => dest.AssignedToUsername, opt => opt.MapFrom(src => src.AssignedTo != null ? src.AssignedTo.Username : null));

            CreateMap<SuratCreateDto, Surat>();
            CreateMap<SuratUpdateDto, Surat>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}