using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Binding = AddonReader.WowTypes.Binding;

namespace AddonReader.Data
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

        public KeyBind(List<Keys> keyList, Binding binding, BindingHeader header)
        {
            KeyList = keyList;
            Binding = binding;
            Header = header;
        }

        public KeyBind(string keys, string name, string header)
        {
            Binding = ParseBinding(name);
            Header = ParseHeader(header);

            if (keys != null)
            {
                keys = keys.Replace("--", "-DASH");

                var keysArray = keys.Split("-");
                foreach (var key in keysArray) KeyList.Add(ParseKeys(key.Replace("DASH", "-")));
            }
        }

        public List<Keys> KeyList { get; } = new List<Keys>();

        public BindingHeader Header { get; }

        public Binding Binding { get; }


        public static KeyBind Parse(string keyBind)
        {
            var data = keyBind.Split(';');
            if (data.Length < 2) return null;

            if (data.Length > 3)
                return new KeyBind(data[3], data[1], data[2]);
            return null;
        }

        private Keys ParseKeys(string key)
        {
            return Enum.TryParse(key, true, out Keys keyValue) ? keyValue : Keys.None;
        }

        private Binding ParseBinding(string binding)
        {
            return Enum.TryParse(binding, true, out Binding bindingValue) ? bindingValue : 0;
        }

        private BindingHeader ParseHeader(string bindingHeader)
        {
            if (bindingHeader.StartsWith("BINDING_HEADER"))
                bindingHeader = bindingHeader.Replace("BINDING_HEADER_", "");
            ;
            return Enum.TryParse(bindingHeader, true, out BindingHeader bindingHeaderValue) ? bindingHeaderValue : 0;
        }
    }
}