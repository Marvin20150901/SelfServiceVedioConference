﻿using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public class DeviceReceiveFilterItem:IDeviceReceiveFilterItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceTypeItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IReceiveFilter<IRequestInfo> ReceiveFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="receiveFilter"></param>
        public DeviceReceiveFilterItem (string type, IReceiveFilter<IRequestInfo> receiveFilter)
        {
            this.DeviceTypeItem = type;
            this.ReceiveFilter = receiveFilter;
        }
    }
}