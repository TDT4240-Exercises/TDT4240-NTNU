using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise2
{
    interface PositionListener
    {
        public void notifyPositionChanged(GameObject gameObject);
    }
}
