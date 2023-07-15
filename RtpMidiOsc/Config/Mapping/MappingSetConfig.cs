using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Mapping
{
    public record MappingSetConfig
    {
        public required string Source { get; set; }
        public required string Target { get; set; }
    }
}