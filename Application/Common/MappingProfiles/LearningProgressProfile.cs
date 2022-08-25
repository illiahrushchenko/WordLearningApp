using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.LearningProgresses.DTOs;
using Application.Quizes.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles
{
    public class LearningProgressProfile : Profile
    {
        public LearningProgressProfile()
        {
            CreateMap<LearningProgressDto, LearningProgress>().ReverseMap();
            CreateMap<LearningProgressItemDto, LearningProgressItem>().ReverseMap();
        }
    }
}
