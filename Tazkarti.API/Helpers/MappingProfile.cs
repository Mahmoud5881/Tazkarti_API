using AutoMapper;
using Tazkarti.API.DTOs;
using Tazkarti.Core.Models;

namespace Tazkarti.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventToReturnDTO>();
        CreateMap<Category, CategoryToReturnDTO>();
        CreateMap<Match, MatchToReturnDTO>();
    }
}