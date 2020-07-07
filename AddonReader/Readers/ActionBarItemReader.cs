using System.Collections.Generic;
using AddonReader.Data;

namespace AddonReader.Readers
{
    public class ActionBarItemReader : IReader<ActionBarItem>
    {
        public ActionBarItem Value { get; }

        public DataFrame SpellInfoFrame { get; }
        public DataFrame SpellIDataFrame { get; }
    }
}