using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kadmium_Osc;
using RtpMidiOsc.Config.Source;

namespace RtpMidiOsc.Target
{
    public class OscTarget : IMidiTarget
    {
        private OscClient OscClient { get; } = new OscClient();
        private OscSourceConfig Config { get; }
        public OscTarget(OscSourceConfig config)
        {
            Config = config;
        }

        public async Task Send(string address, float value)
        {
            var packet = new OscMessage(address, value);
            await OscClient.Send(Config.Host, Config.Port, packet);
        }
    }
}