using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Source;
using RtpMidiOsc.Mapping;

namespace RtpMidiOsc.Config.Mapping
{
    public record OscMappingItemConfig : MappingItemConfig
    {
        public override SourceType Type { get { return SourceType.OSC; } }
        public required string Address { get; set; }
        public required OscArgType ArgType { get; set; }
    }
}