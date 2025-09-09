using AutoMapper;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Will be populated separately

            CreateMap<User, UserSummaryDto>();

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Letter mappings
            CreateMap<CreateLetterDto, Letter>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LetterNumber, opt => opt.Ignore()) // Will be generated
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => LetterStatus.Draft))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.SenderId, opt => opt.Ignore()) // Will be set from current user
                .ForMember(dest => dest.Sender, opt => opt.Ignore())
                .ForMember(dest => dest.Recipient, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore());

            CreateMap<UpdateLetterDto, Letter>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LetterNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.SenderId, opt => opt.Ignore())
                .ForMember(dest => dest.SentAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReceivedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.Ignore())
                .ForMember(dest => dest.Recipient, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore());

            CreateMap<Letter, LetterDto>();
            CreateMap<Letter, LetterSummaryDto>();

            // Letter Attachment mappings
            CreateMap<LetterAttachment, LetterAttachmentDto>();
        }
    }
}