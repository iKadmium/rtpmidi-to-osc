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
                var source = Sources[mappingSet.Source] ?? throw new InvalidDataException($"Could not find source {mappingSet.Source}");
                var target = Targets[mappingSet.Target] ?? throw new InvalidDataException($"Could not find target {mappingSet.Target}");

                foreach (var map in mappingSet.Map)
                {
                    source.AddListener(map.From, target.GetSendFunc(map.To));
                }
            }
        }
    }
}