using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application
{
    interface IStrategy
    {
        void UpdateData(int itemPos, string itemName, IUpdateInfo update);
    }
}
