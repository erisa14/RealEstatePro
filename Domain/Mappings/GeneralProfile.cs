using AutoMapper;
using DTO.Property;
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
        #region Property

        public GeneralProfile() {
        CreateMap<Property,EditPropertyDTO>().ReverseMap();
        CreateMap<Property,PropertyDTO>().ReverseMap();

            CreateMap<PropertyDTO, Property>()
           .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.CategoryType.ToString()));

            CreateMap<Photo, PhotoDTO>().ReverseMap();
        } 

        

        #endregion
    }
}
