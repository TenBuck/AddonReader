using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TenBot.Game.WowEntities;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.Frames
{ 
    public class DataFramesMgr
    {
        public List<DataFrame> DataFrames { get; set; }

        public DataFramesMgr(List<DataFrame> dataFrames)
        {
            DataFrames = dataFrames;
        }

       
    }
}
