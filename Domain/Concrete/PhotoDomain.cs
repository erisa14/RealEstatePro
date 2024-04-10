using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.Property;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class PhotoDomain : DomainBase, IPhotoDomain
    {

        public PhotoDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IPropertyRepository propertyRepository => _unitOfWork.GetRepository<IPropertyRepository>();

        private IPhotoRepository photoRepository => _unitOfWork.GetRepository<IPhotoRepository>();


        public async Task UploadImageAsync(PhotoDTO photoDTO)
        {

            var property = propertyRepository.GetById(photoDTO.PropertyId);
            if (property == null)
            {
                throw new Exception("The referenced property does not exist.");
            }

            if (photoDTO.ImageFile.Count < 5)
            {
                throw new Exception("At least 5 files must be uploaded.");
            }

            foreach (var imageFile in photoDTO.ImageFile)
            {
                var tempPhotoDTO = new PhotoDTO
                {
                    PropertyId = photoDTO.PropertyId,
                    ImageFile = photoDTO.ImageFile
                };

                var photo = _mapper.Map<Photo>(tempPhotoDTO);

                photo.Path = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    photo.Data = memoryStream.ToArray();
                }

                photoRepository.Add(photo);
            }

            _unitOfWork.Save();
        }


    }
}