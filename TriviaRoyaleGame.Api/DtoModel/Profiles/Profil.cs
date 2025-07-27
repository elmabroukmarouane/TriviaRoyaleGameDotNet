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
                .ForMember(dest => dest.Users, opt => opt.Ignore());

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
                .ForMember(dest => dest.CategoryViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryViewModel, opt =>
                {
                    opt.PreCondition(src => src.Category != null);
                    opt.MapFrom(src => src.Category);
                })
                .ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Avoid loop

            // ClientAppLog - QuestionViewModel
            CreateMap<ClientAppLog, ClientAppLogViewModel>()
                .ReverseMap();

            // CryptoPayload - CryptoPayloadViewModel
            CreateMap<CryptoPayload, CryptoPayloadViewModel>()
                .ReverseMap();

            // ScoreBoard -> ScoreBoardViewModel
            CreateMap<ScoreBoard, ScoreBoardViewModel>()
                .ForMember(dest => dest.UserViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.UserViewModel, opt =>
                {
                    opt.PreCondition(src => src.User != null);
                    opt.MapFrom(src => src.User);
                })
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore()); // Avoid loop

            // Category -> CategoryViewModel
            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.QuestionViewModels, opt => opt.Ignore())
                .ForMember(dest => dest.QuestionViewModels, opt =>
                {
                    opt.PreCondition(src => src.Questions != null);
                    opt.MapFrom(src => src.Questions);
                })
                .ReverseMap()
                .ForMember(dest => dest.Questions, opt => opt.Ignore());
        }
    }
}
