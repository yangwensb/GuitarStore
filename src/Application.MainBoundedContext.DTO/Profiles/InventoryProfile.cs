using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.MainBoundedContext.DTO;

namespace Application.MainBoundedContext.DTO.Profiles
{
    public class InventoryProfile : Profile
    {
        protected override void Configure()
        {
            //object[] => InventoryListDTO
            var map = Mapper.CreateMap<object[], InventoryListDto>();
            map.ForMember(dto => dto.Id, mc => mc.MapFrom(o => Guid.Parse(Convert.ToString(o[0]))));
            map.ForMember(dto => dto.Type, mc => mc.MapFrom(o => Convert.ToString(o[1])));
            map.ForMember(dto => dto.Builder, mc => mc.MapFrom(o => Convert.ToString(o[2])));
            map.ForMember(dto => dto.Model, mc => mc.MapFrom(o => Convert.ToString(o[3])));
            map.ForMember(dto => dto.QOH, mc => mc.MapFrom(o => Convert.ToInt32(o[4])));
            map.ForMember(dto => dto.Cost, mc => mc.MapFrom(o => Convert.ToDecimal(o[5])));
            map.ForMember(dto => dto.Price, mc => mc.MapFrom(o => Convert.ToDecimal(o[6])));
            map.ForMember(dto => dto.Received, mc => mc.MapFrom(o => Convert.ToDateTime(o[7])));
        }
    }
}

