using dbaccess.Extensions;
using dbaccess.Common;
using dbaccess.Models;
using dbaccess.Factory;
using dbaccess.Mapper;
using dbaccess.Repository;
using dbaccess.Repository.QueryEnrichment;
using dbaccess.Factory.Test;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using System;

namespace dbaccess.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            services.AddDbAccessServices(null);
        }
    }
}