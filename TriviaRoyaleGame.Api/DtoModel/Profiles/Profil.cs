using AutoMapper;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.Api.DtoModel.Models;

namespace TriviaRoyaleGame.Api.DtoModel.Profiles
{
    public class Profil : Profile
    {
        public Profil()
        {
            // Member -> MemberViewModel
            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.UserViewModels, opt => opt.Ignore())
                .ForMember(dest => dest.UserViewModels, opt =>
                {
                    opt.PreCondition(src => src.Users != null);
                    opt.MapFrom(src => src.Users);
                })
                .ReverseMap()
                .ForMember(dest => dest.Users, opt => opt.Ignore()); ;

            // User -> UserViewModel
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.MemberViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.MemberViewModel, opt =>
                {
                    opt.PreCondition(src => src.Member != null);
                    opt.MapFrom(src => src.Member);
                })
                .ReverseMap()
                .ForMember(dest => dest.Member, opt => opt.Ignore()); // Avoid loop

            // Question - QuestionViewModel
            CreateMap<Question, QuestionViewModel>()
                .ReverseMap();

            // ClientAppLog - QuestionViewModel
            CreateMap<ClientAppLog, ClientAppLogViewModel>()
                .ReverseMap();

            // CryptoPayload - CryptoPayloadViewModel
            CreateMap<CryptoPayload, CryptoPayloadViewModel>()
                .ReverseMap();
        }
    }
}
