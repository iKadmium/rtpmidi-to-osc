using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Source;

namespace RtpMidiOsc.Config.Mapping
{
    public record RtpMappingItemConfig : MappingItemConfig
    {
        public override SourceType Type { get { return SourceType.RTP; } }
        public required byte Cc { get; set; }
        public required byte Channel { get; set; }
    }
}