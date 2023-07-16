using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using RtpMidiOsc.JsonOptions;

namespace RtpMidiOsc.Config
{
    public class JsonConfigProvider : IConfigProvider
    {
        private IJsonOptionsProvider JsonOptionsProvider { get; }
        public JsonConfigProvider(IJsonOptionsProvider optionsProvider)
        {
            JsonOptionsProvider = optionsProvider;
        }

        public async Task<Config> GetConfigAsync()
        {
            var filename = Path.Combine("..", "config", "config.json");
            var fullPath = Path.GetFullPath(filename);
            var body = await File.ReadAllTextAsync(fullPath);
            if (body == null)
            {
                throw new FileLoadException("Unable to load /config/config.json");
            }
            var config = JsonSerializer.Deserialize<Config>(body, JsonOptionsProvider.GetJsonSerializerOptions());
            if (config == null)
            {
                throw new JsonException("Unable to parse config file");
            }

            return config;
        }
    }
}