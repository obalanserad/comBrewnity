using AutoMapper;
using ComBrewnityV2.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComBrewnityV2.Mapping
{
    public class DataMapping : Profile
        {
            public DataMapping()
            {

            CreateMap<ApplicationUser, UserDto>()
                .ForMember(x=>x.Email, opt=>opt.MapFrom(z=>z.UserName))
                .ReverseMap();
            CreateMap<Post, PostDto>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(o => o.User.Id))
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(o=>o.User.UserName))
                .ReverseMap();
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<TasteNote, TasteNoteDto>().ReverseMap();
            CreateMap<TechnicalData, TechnicalDataDto>().ReverseMap();
            CreateMap<MashStep, MashStepDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Style, StyleDto>().ReverseMap();



        }
    }
    
}
