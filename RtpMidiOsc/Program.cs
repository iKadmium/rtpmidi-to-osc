// See https://aka.ms/new-console-template for more information
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RtpMidiOsc.Config;
using RtpMidiOsc.JsonOptions;
using RtpMidiOsc.Mapping;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IConfigProvider, JsonConfigProvider>();
builder.Services.AddTransient<IJsonOptionsProvider, JsonOptionsProvider>();
builder.Services.AddSingleton<IMappingManager, MappingManager>();

using IHost host = builder.Build();
var mappingManager = host.Services.GetRequiredService<IMappingManager>();
await mappingManager.Setup();

await host.RunAsync();