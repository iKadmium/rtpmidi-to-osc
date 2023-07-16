using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config.Source
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(RtpSourceConfig), typeDiscriminator: "rtp")]
    [JsonDerivedType(typeof(OscSourceConfig), typeDiscriminator: "osc")]
    public abstract record SourceConfig
    {
        public abstract SourceType Type { get; }
        public required string Name { get; set; }
        public required ushort Port { get; set; }
        public string? Host { get; set; }
    }
}