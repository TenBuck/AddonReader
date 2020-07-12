using Microsoft.Extensions.DependencyInjection;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.ActionBars;
using TenBot.AddonReader.Readers.Unit;

namespace TenBot.Extensions.Services
{
    public static class ReadingServices
    {
        public static void AddReaders(this IServiceCollection services)
        {
            services.AddSingleton<PlayerReader>();
            services.AddSingleton<TargetOfTarget>();
            services.AddSingleton<TargetReader>();
            services.AddSingleton<PositionReader>();
            services.AddSingleton<AuraReader>()
                ;
            services.AddSingleton<AddonReaderMgr>();
            services.AddSingleton<ActionsReader>();
        }
    }
}