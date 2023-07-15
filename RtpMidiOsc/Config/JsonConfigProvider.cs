using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace RtpMidiOsc.Config
{
    public class JsonConfigProvider : IConfigProvider
    {
        public async Task<Config> GetConfig()
        {
            var filename = Path.Combine("..", "config", "config.json");
            var fullPath = Path.GetFullPath(filename);
            var body = await File.ReadAllTextAsync(fullPath);
            var config = JsonSerializer.Deserialize<Config>(body);

            return config;
        }
    }
}