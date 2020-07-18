using Microsoft.Extensions.DependencyInjection;
using TenBot.AddonReader;
using TenBot.AddonReader.Boxes;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.Units;
using TenBot.AddonReader.SavedVariables;

namespace TenBot.Extensions.Services
{
    public static class ReadingServices
    {
        public static void AddReaders(this IServiceCollection services)
        {
            services.AddSingleton<BoxMgr>();
            services.AddSingleton<BoxBuilder>();

            services.AddSingleton<PlayerReader>();
            services.AddSingleton<TargetOfTarget>();
            services.AddSingleton<TargetReader>();
            services.AddSingleton<PositionReader>();
            services.AddSingleton<AuraReader>();
            services.AddSingleton<AddonReaderMgr>();
            services.AddSingleton<ActionsReader>();

            services.AddSingleton<AddonConfigProvider>();
            services.AddSingleton<SavedVariablesParser>(s => new SavedVariablesParser("Licella", "Netherwind"));
        }
    }
}