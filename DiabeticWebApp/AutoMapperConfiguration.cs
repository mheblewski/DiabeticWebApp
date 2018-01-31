using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using AutoMapper;
using DiabeticWebApp.Repository;

namespace DiabeticWebApp
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AutomapperProfiles>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}