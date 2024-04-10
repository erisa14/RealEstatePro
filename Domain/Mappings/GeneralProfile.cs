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
        #region User
        public GeneralProfile()
        {
           CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Photo, PhotoDTO>().ReverseMap();
        } 

        

        #endregion
    }
}
