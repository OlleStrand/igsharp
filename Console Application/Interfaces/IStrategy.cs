using Lightstreamer.DotNet.Client;

namespace IgBotTraderCLI
{
    internal interface IStrategy
    {
        void UpdateData(int itemPos, string itemName, IUpdateInfo update);
    }
}