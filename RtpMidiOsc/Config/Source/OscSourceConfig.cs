using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Source
{
    public record OscSourceConfig : SourceConfig
    {
        public override SourceType Type { get { return SourceType.OSC; } }
    }
}