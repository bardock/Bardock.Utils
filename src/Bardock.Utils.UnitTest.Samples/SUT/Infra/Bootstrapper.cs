using AutoMapper;
using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Mapper.CreateMap<CustomerCreate, Customer>();
        }
    }
}
