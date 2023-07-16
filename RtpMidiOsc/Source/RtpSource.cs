using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KadmiumRtpMidi;
using KadmiumRtpMidi.Packets;
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
    }
}