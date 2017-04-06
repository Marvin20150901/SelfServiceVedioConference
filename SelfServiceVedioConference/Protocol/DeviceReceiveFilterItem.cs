using System;
using SelfServiceVedioConference.Device;
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
        public Type ReceiveFilter { get; set; }

        public Type DeviceDriver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="receiveFilter"></param>
        public DeviceReceiveFilterItem (string type, Type receiveFilter, Type deviceDriverType)
        {
            this.DeviceTypeItem = type;
            this.ReceiveFilter = receiveFilter;
            this.DeviceDriver = deviceDriverType;
        }
    }
}