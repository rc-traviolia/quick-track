using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickTrackWeb.Shared.Models;
using QuickTrackWeb.EmbeddedApi.Entities;

namespace QuickTrackWeb.EmbeddedApi.Services
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            //go from the Entity (LEFT) to the Dto (RIGHT)
            CreateMap<ClassEntity, ClassEntityDto>();
            CreateMap<ClassEntity, ClassEntityWithoutChildrenDto>();
            CreateMap<ClassEntityForCreationDto, ClassEntity>();

            CreateMap<Student, StudentDto>();
            CreateMap<Student, StudentWithoutProgressDto>();
            CreateMap<StudentForCreationDto, Student>();

            //EXAMPLE:
            //LEFT: Source to change
            //RIGHT: Target to change into
            //Dto's - Only need to 
            //CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
            //CreateMap<Entities.City, Models.CityDto>();
            //CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            //CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            //CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
            //CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
        }
    }
}
