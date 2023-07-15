using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Source;

namespace RtpMidiOsc.Config.Mapping
{
    public record MappingItemConfig
    {
        public required SourceType Type { get; set; }
    }
}