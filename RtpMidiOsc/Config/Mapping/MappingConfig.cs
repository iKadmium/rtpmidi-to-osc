using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Mapping
{
    public record MappingConfig
    {
        public required string From { get; set; }
        public required string To { get; set; }
    }
}