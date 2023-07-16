using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KadmiumRtpMidi;
using KadmiumRtpMidi.Packets;
using KadmiumRtpMidi.Packets.MidiCommands;
using RtpMidiOsc.Config.Mapping;
using RtpMidiOsc.Config.Source;

namespace RtpMidiOsc.Source
{
    public class RtpSource : IMidiSource
    {
        private Session Session { get; }
        public event EventHandler<PacketReceivedEventArgs>? OnPacketReceived;

        public RtpSource(RtpSourceConfig config)
        {
            Session = new KadmiumRtpMidi.Session(IPAddress.Any, config.Port, config.BonjourName);
            Session.OnPacketReceived += (sender, args) =>
            {
                OnPacketReceived?.Invoke(this, args);
            };
        }

        public void AddListener(MappingItemConfig config, Func<byte, Task> sendFunc)
        {
            if (config is RtpMappingItemConfig rtpMappingItemConfig)
            {
                Session.OnPacketReceived += async (sender, packet) =>
                {
                    var matches = packet.Packet.Commands.Where(x => x is ControlChange cc
                        && cc.CcNumber == rtpMappingItemConfig.Cc
                        && cc.Channel == rtpMappingItemConfig.Channel
                    ).Select(x => x as ControlChange);
                    foreach (var match in matches)
                    {
                        if (match != null)
                        {
                            await sendFunc(match.Value);
                        }
                    }
                };
            }
            else
            {
                throw new InvalidDataException($"Tried to map {config.Type} data from an RTP Source");
            }
        }
    }
}