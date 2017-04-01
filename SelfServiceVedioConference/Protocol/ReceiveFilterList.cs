using System;
using System.Collections.Generic;
using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public class ReceiveFilterList: IReceiveFilterList
    {
        public Dictionary<string,Type> ResceiveFilterDic{ get; set; }

        public ReceiveFilterList()
        {
            ResceiveFilterDic=new Dictionary<string, Type>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceReceiveFilterItem"></param>
        public void AddDeviceRecieveFilterr(IDeviceReceiveFilterItem  deviceReceiveFilterItem)
        {
            ResceiveFilterDic.Add(deviceReceiveFilterItem.DeviceTypeItem,deviceReceiveFilterItem.ReceiveFilter);            
        }

    }
}