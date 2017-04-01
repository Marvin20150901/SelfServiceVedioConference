using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public interface IReceiveFilterList
    {
        Dictionary<string,Type> ResceiveFilterDic { get; }
        void AddDeviceRecieveFilterr(IDeviceReceiveFilterItem deviceReceiveFilterItem);

    }



}
