using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Mapping;
using RtpMidiOsc.Config.Source;

namespace RtpMidiOsc.Config
{
    public record Config
    {
        public IList<SourceConfig> Sources { get; set; } = new List<SourceConfig>();
        public IList<SourceConfig> Targets { get; set; } = new List<SourceConfig>();
        public IList<MappingSetConfig> MappingSets { get; set; } = new List<MappingSetConfig>();
    }
}