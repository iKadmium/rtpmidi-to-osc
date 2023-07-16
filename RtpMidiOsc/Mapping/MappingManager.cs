using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KadmiumRtpMidi.Packets.MidiCommands;
using RtpMidiOsc.Config;
using RtpMidiOsc.Config.Mapping;
using RtpMidiOsc.Config.Source;
using RtpMidiOsc.Source;
using RtpMidiOsc.Target;

namespace RtpMidiOsc.Mapping
{
    public class MappingManager : IMappingManager
    {
        public MappingManager(IConfigProvider configProvider)
        {
            this.ConfigProvider = configProvider;

        }
        private IConfigProvider ConfigProvider { get; }
        private IDictionary<string, IMidiSource> Sources { get; } = new Dictionary<string, IMidiSource>();
        private IDictionary<string, IMidiTarget> Targets { get; } = new Dictionary<string, IMidiTarget>();
        private IList<Mapping> Mappings { get; } = new List<Mapping>();

        public async Task Setup()
        {
            var config = await ConfigProvider.GetConfigAsync();
            foreach (var sourceConfig in config.Sources)
            {
                switch (sourceConfig)
                {
                    case RtpSourceConfig rtpSourceConfig:
                        var rtpSource = new RtpSource(rtpSourceConfig);
                        Sources.Add(rtpSourceConfig.Name, rtpSource);
                        break;
                }
            }
            foreach (var targetConfig in config.Targets)
            {
                switch (targetConfig)
                {
                    case OscSourceConfig oscSourceConfig:
                        var oscTarget = new OscTarget(oscSourceConfig);
                        Targets.Add(oscSourceConfig.Name, oscTarget);
                        break;
                }
            }
            foreach (var mappingSet in config.MappingSets)
            {
                var source = Sources[mappingSet.Source];
                var target = Targets[mappingSet.Target];
                foreach (var map in mappingSet.Map)
                {
                    Func<byte, Task>? sendFunc = null;
                    if (map.To is OscMappingItemConfig oscMappingItemConfig && target is OscTarget oscTarget)
                    {
                        sendFunc = (value) => oscTarget.Send(oscMappingItemConfig.Address, value);
                    }
                    if (sendFunc != null)
                    {
                        switch (source)
                        {
                            case RtpSource rtpSource:
                                if (map.From is RtpMappingItemConfig rtpMappingItemConfig)
                                {
                                    rtpSource.OnPacketReceived += async (sender, packet) =>
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
                                break;
                        }
                    }
                }
            }
        }
    }
}