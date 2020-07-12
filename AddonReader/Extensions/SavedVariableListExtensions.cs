using System.Collections.Generic;
using System.Linq;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Game.WowTypes;

namespace TenBot.Extensions
{
    public static class SavedVariableListExtensions
    {
        public static Dictionary<KeyBinding, KeyBind> ParseKeyBind(this SavedVariable keyBinds)
        {
            return keyBinds.Fields.ConvertAll(KeyBind.Parse)
                .ToDictionary(x => x.KeyBinding, x => x);
        }
    }
}