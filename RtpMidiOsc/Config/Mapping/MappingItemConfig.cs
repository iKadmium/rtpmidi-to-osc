using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RtpMidiOsc.Config.Source;

namespace RtpMidiOsc.Config.Mapping
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(RtpMappingItemConfig), typeDiscriminator: "rtp")]
    [JsonDerivedType(typeof(OscMappingItemConfig), typeDiscriminator: "osc")]
    public abstract record MappingItemConfig
    {
        public abstract SourceType Type { get; }
    }
}