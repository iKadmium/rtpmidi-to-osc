using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Mapping;

namespace RtpMidiOsc.Source
{
    public interface IMidiSource
    {
        void AddListener(MappingItemConfig config, Func<byte, Task> sendFunc);
    }
}