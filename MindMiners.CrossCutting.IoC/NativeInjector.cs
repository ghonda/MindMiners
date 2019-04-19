﻿using Microsoft.Extensions.DependencyInjection;
using MindMiners.Application;
using MindMiners.CrossCutting.Infrastructure.Services;
using MindMiners.Data.Repositories;
using MindMiners.Domain.Interfaces;

namespace MindMiners.CrossCutting.IoC
{
    public sealed class NativeInjector
    {
        public static void Register(IServiceCollection service)
        {
            service.AddScoped<IHistoryApplication, HistoryApplication>();
            service.AddScoped<ISynchronizationApplication, SynchronizationApplication>();
            service.AddScoped<ISrtParser, SrtParser>();
            service.AddScoped<IFileHistoryRepository, FileHistoryRepository>();
            
        }
    }
}
