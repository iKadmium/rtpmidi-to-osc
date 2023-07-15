using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtpMidiOsc.Config
{
    public interface IConfigProvider
    {
        public Task<Config> GetConfig();
    }
}