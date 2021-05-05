using AutoMapper;
using Loris.Application.Dtos;
using Loris.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Loris.Application
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AuthUser, UserDto>()
                .ForMember(dest => dest.Roles, opt =>
                {
                    opt.MapFrom(src => src.AuthUserRole.Select(x => x.AuthRole).ToList());
                })
                .ReverseMap();

            CreateMap<AuthRole, RoleDto>()
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.AuthRoleResource))
                .ReverseMap();

            CreateMap<AuthRoleResource, RoleResourceDto>()
                .ForMember(dest => dest.Resource, opt => opt.MapFrom(src => src.AuthResource))
                .ReverseMap();

            CreateMap<AuthResource, ResourceDto>()
                .ReverseMap();

            CreateMap<CourierTemplate, CourierTemplateDto>()
                .ReverseMap();

            CreateMap<CourierMessage, CourierMessageDto>()
                .ReverseMap();

            CreateMap<CourierTo, CourierToDto>()
                .ReverseMap();

            CreateMap<CourierAttachment, CourierAttachmentDto>()
                .ReverseMap();
        }
    }

    public static class AutoMappingExtension
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
    }
}
