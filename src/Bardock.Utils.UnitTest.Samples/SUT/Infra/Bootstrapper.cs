using AutoMapper;
using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;

namespace Bardock.Utils.UnitTest.Samples.SUT.Infra
{
    public class Bootstrapper
    {
        public static void Init()
        {
            InitMappings();
        }

        private static void InitMappings()
        {
            Mapper.CreateMap<CustomerCreate, Customer>()
                .ForMember(c => c.LastName, o => o.MapFrom(dto => dto.Surname))
                .ReverseMap();

            Mapper.CreateMap<CustomerUpdate, Customer>()
                .ForMember(c => c.ID, o => o.Ignore())
                .ForMember(c => c.LastName, o => o.MapFrom(dto => dto.Surname))
                .ReverseMap();
        }
    }
}