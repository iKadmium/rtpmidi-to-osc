using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Mapping
{
    public record MappingConfig
    {
        public required MappingItemConfig From { get; set; }
        public required MappingItemConfig To { get; set; }
    }
}