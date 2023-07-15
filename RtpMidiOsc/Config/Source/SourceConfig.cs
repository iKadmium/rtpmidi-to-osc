using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Source
{
    public abstract record SourceConfig
    {
        public required SourceType Type { get; set; }
        public required string Name { get; set; }
        public required ushort Port { get; set; }
        public string? Hostname { get; set; }
    }
}