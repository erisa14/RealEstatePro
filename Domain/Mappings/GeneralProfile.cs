using AutoMapper;
using DTO.Property;
using DTO.UserDTO;
using Entities.Models;
using LamarCodeGeneration.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappings
{
    public class GeneralProfile : Profile
    {
        #region User
        public GeneralProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<UserRole, RoleDto>().ReverseMap();
            CreateMap<RegisterDto, User>().ForMember(dest => dest.UserRoles, opt => opt.Ignore());




            CreateMap<Property, EditPropertyDTO>().ReverseMap();
            CreateMap<Property, PropertyDTO>().ReverseMap();

            CreateMap<PropertyDTO, Property>()
           .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.CategoryType.ToString()));

            CreateMap<Photo, PhotoDTO>().ReverseMap();
        }
        #endregion

        
        
            
        
    }
}
