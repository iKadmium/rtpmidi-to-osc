using System.Text.Json.Serialization;

namespace RtpMidiOsc.Config.Source
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SourceType
    {
        RTP,
        OSC
    }
}