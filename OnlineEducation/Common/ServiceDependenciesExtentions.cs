﻿using OnlineEducation.BLL.Services;
using OnlineEducation.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineEducation.Common
{
    public static class ServiceDependenciesExtentions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IGroupService, GroupService>();

            return services;
        }
    }
}