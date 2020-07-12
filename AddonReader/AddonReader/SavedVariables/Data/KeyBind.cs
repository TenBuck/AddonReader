using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GregsStack.InputSimulatorStandard.Native;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.SavedVariables.Data
{
    public class KeyBind
    {
        public enum BindingHeader
        {
            MOVEMENT,
            CHAT,
            ACTIONBAR,
            MULTIACTIONBAR,
            TARGETING,
            INTERFACE,
            MISC,
            CAMERA,
            RAIDTARGET
        }

        public KeyBind(List<VirtualKeyCode> keyList, KeyBinding keyBinding, BindingHeader header)
        {
            KeyList = keyList;
            KeyBinding = keyBinding;
            Header = header;
        }

        public KeyBind(string? keys, string name, string header)
        {
            KeyBinding = ParseBinding(name);
            Header = ParseHeader(header);

            if (keys != null)
            {
                keys = keys.Replace("--", "-DASH");
                var keysArray = keys.Split("-");
                foreach (var key in keysArray) KeyList.Add(ParseKeys(key.Replace("DASH", "-")));
            }
        }


        public List<VirtualKeyCode> KeyList { get; } = new List<VirtualKeyCode>();
        public BindingHeader Header { get; }

        public KeyBinding KeyBinding { get; }

        public static KeyBind Parse(string keyBind)
        {
            var data = keyBind.Split(';');

            return data.Length < 4
                ? new KeyBind(null, data[1], data[2])
                : new KeyBind(data[3], data[1], data[2]);
        }

        private static VirtualKeyCode ParseKeys(string key)
        {
            switch (key)
            {
                case "SHIFT":
                    return VirtualKeyCode.SHIFT;
                case "CTRL":
                    return VirtualKeyCode.CONTROL;
                case "ALT":
                    return VirtualKeyCode.MENU;
                case "0":
                    return VirtualKeyCode.VK_0;
                case "1":
                    return VirtualKeyCode.VK_1;
                case "2":
                    return VirtualKeyCode.VK_2;
                case "3":
                    return VirtualKeyCode.VK_3;
                case "4":
                    return VirtualKeyCode.VK_4;
                case "5":
                    return VirtualKeyCode.VK_5;
                case "6":
                    return VirtualKeyCode.VK_6;
                case "7":
                    return VirtualKeyCode.VK_7;
                case "8":
                    return VirtualKeyCode.VK_8;
                case "9":
                    return VirtualKeyCode.VK_9;
            }

            return Enum.TryParse(key, true, out Keys keyValue)
                ? (VirtualKeyCode) keyValue
                : (VirtualKeyCode) Keys.None;
        }

        private static KeyBinding ParseBinding(string binding)
        {
            return Enum.TryParse(binding, true, out KeyBinding bindingValue) ? bindingValue : 0;
        }

        private static BindingHeader ParseHeader(string bindingHeader)
        {
            if (bindingHeader.StartsWith("BINDING_HEADER"))
                bindingHeader = bindingHeader.Replace("BINDING_HEADER_", "");

            return Enum.TryParse(bindingHeader, true, out BindingHeader bindingHeaderValue) ? bindingHeaderValue : 0;
        }
    }
}