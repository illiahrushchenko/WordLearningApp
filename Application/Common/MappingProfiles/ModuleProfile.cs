using Application.Modules.Commands.CreateModule;
using Application.Modules.Commands.UpdateModule;
using Application.Modules.Queries.GetModuleDetails;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<CreateModuleCommand, Module>().ReverseMap();
            CreateMap<CreateCardCommand, Card>().ReverseMap();
            CreateMap<UpdateModuleCommand, Module>().ReverseMap();
            CreateMap<UpdateCardCommand, Card>().ReverseMap();
            CreateMap<ModuleDetailsDto, Module>().ReverseMap();
            CreateMap<CardDto, Card>().ReverseMap();
        }
    }
}
