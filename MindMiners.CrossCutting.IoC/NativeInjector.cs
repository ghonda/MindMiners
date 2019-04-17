using Microsoft.Extensions.DependencyInjection;
using MindMiners.Application;
using MindMiners.CrossCutting.Infrastructure.Services;
using MindMiners.Domain.Interfaces;

namespace MindMiners.CrossCutting.IoC
{
    public sealed class NativeInjector
    {
        public static void Register(IServiceCollection service)
        {
            service.AddScoped<ISynchronizationApplication, SynchronizationApplication>();
            service.AddScoped<ISrtParser, SrtParser>();
        }
    }
}
