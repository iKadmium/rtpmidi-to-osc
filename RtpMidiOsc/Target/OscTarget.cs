using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Kadmium_Osc;
using RtpMidiOsc.Config.Mapping;
using RtpMidiOsc.Config.Source;
using RtpMidiOsc.Mapping;

namespace RtpMidiOsc.Target
{
    public class OscTarget : IMidiTarget
    {
        private UdpClient UdpClient { get; }

        public OscTarget(OscSourceConfig config)
        {
            UdpClient = new UdpClient(config.Host ?? throw new Exception("Hostname not provided"), config.Port);
        }

        public async Task Send(string address, float value)
        {
            var packet = new OscMessage(address, value);
            using var owner = System.Buffers.MemoryPool<byte>.Shared.Rent((int)packet.Length);
            var memory = owner.Memory.Slice(0, (int)packet.Length);
            packet.Write(memory.Span);
            await UdpClient.SendAsync(memory);
        }

        public async Task Send(string address, int value)
        {
            var packet = new OscMessage(address, value);
            using var owner = System.Buffers.MemoryPool<byte>.Shared.Rent((int)packet.Length);
            var memory = owner.Memory.Slice(0, (int)packet.Length);
            packet.Write(memory.Span);
            await UdpClient.SendAsync(memory);
        }

        public Func<byte, Task> GetSendFunc(MappingItemConfig config)
        {
            if (config is OscMappingItemConfig oscMappingItemConfig)
            {
                switch (oscMappingItemConfig.ArgType)
                {
                    case OscArgType.Float:
                        return (byte value) =>
                        {
                            float floatVal = (value / 127f);
                            return Send(oscMappingItemConfig.Address, floatVal);
                        };
                    case OscArgType.Int:
                        return (byte value) =>
                        {
                            int intVal = value;
                            return Send(oscMappingItemConfig.Address, intVal);
                        };
                    default:
                        throw new ArgumentException($"OscArgType {oscMappingItemConfig.ArgType} not supported");
                }

            }
            else
            {
                throw new InvalidDataException($"Tried to map {config.Type} data to an OSC Target");
            }
        }
    }
}