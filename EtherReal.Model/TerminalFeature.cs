using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherReal.Model
{
    [Flags]
    public enum TerminalFeature
    {
        None,
        GPS,
        Audio,
        Video,
        Wifi,
        BLE,
        Keyboard
    }
}
