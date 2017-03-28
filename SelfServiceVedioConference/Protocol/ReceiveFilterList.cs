using System.Collections.Generic;
using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public class ReceiveFilterList: IReceiveFilterList
    {
        public Dictionary<DeviceType, IReceiveFilter<IRequestInfo>> ResceiveFilterDic{ get; set; }

        public ReceiveFilterList()
        {
            
        }


    }
}