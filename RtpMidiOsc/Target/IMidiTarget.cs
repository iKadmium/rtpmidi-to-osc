using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Mapping;

namespace RtpMidiOsc.Target
{
    public interface IMidiTarget
    {
        Func<byte, Task> GetSendFunc(MappingItemConfig config);
    }
}