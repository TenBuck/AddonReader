using Microsoft.Extensions.DependencyInjection;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.Units;
using TenBot.Core.WowPlayer;

namespace TenBot.Extensions.Services
{
    public static class PlayerServices
    {
        public static void AddPlayer(this IServiceCollection services)
        {
            services.AddSingleton<Player>();
            services.AddSingleton<PlayerTarget>();
            services.AddSingleton<PlayerAuras>();
            services.AddSingleton<PlayerSpells>();
            services.AddSingleton<MouseServices>();

        }
    }
}