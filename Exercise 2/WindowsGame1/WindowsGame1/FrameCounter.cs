using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise2
{
    class FrameCounter : Observer
    {
        private UInt64 frameCounter;

        public FrameCounter()
        {
            frameCounter = 0;
        }

        public UInt64 getCounter()
        {
            return frameCounter;
        }

        public void notify()
        {
            frameCounter++;
        }
    }
}
