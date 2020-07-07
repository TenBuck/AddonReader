using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddonReader.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AddonReader.Data.Tests
{
    [TestClass()]
    public class KeyBindingTests
    {
        [TestMethod()]
        public void ParseKeysTest()
        {
            var keys = "SHIFT";//"-SPACE";

            var kb = KeyBind.ParseKeys(keys);
            Assert.AreEqual(kb, Keys.Shift);
        }
        [TestMethod()]
        public void ParseKeysKCTest()
        {
            KeysConverter kc = new KeysConverter();
            var invarient = kc.ConvertToInvariantString(Keys.Shift);
            var variant = kc.ConvertToInvariantString(Keys.Shift);

            var key = kc.ConvertFrom("SHIFT-");
            Assert.AreEqual(Keys.Shift.ToString(), "Shift");
        }


    }

}