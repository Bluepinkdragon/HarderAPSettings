using System;

namespace HarderAPSettings
{
    [Serializable]
    public class LocalSettings
    {
        public int ReduceNail = 0;
        public int ReduceMasks = 0 ;

        // 0=default/off, 1=skong, 2=double
        public int ExtraDamage = 0;

        public bool HardMode = false;
    }
}