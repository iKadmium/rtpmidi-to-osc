using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RtpMidiOsc.JsonOptions
{
    public interface IJsonOptionsProvider
    {
        public JsonSerializerOptions GetJsonSerializerOptions();
    }
}