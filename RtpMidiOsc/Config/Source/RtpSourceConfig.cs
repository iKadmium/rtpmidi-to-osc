using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Source
{
    public record RtpSourceConfig : SourceConfig
    {
        public required string BonjourName { get; set; }

        public override SourceType Type { get { return SourceType.RTP; } }
    }
}