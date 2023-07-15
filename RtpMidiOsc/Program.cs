// See https://aka.ms/new-console-template for more information
using System.Net;
using RtpMidiOsc.Config;

Console.WriteLine("Hello, World!");
await new JsonConfigProvider().GetConfig();