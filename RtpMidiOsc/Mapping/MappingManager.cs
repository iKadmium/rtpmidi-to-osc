using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtpMidiOsc.Config;
using RtpMidiOsc.Config.Source;
using RtpMidiOsc.Source;

namespace RtpMidiOsc.Mapping
{
    public class MappingManager : IMappingManager
    {
        public MappingManager(IConfigProvider configProvider)
        {
            this.ConfigProvider = configProvider;

        }
        private IConfigProvider ConfigProvider { get; }
        private IDictionary<string, MidiSource> Sources { get; } = new Dictionary<string, MidiSource>();

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
        }
    }
}