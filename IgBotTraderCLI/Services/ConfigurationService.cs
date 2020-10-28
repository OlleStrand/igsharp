using IgBotTraderCLI.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace IgBotTraderCLI.Services
{
    public static class ConfigurationService
    {
        public static IGApiAccount LoadAccountDetails() => JsonConvert.DeserializeObject<IGApiAccount>(
            File.ReadAllText(Environment.CurrentDirectory + "\\config.json"));
    }
}