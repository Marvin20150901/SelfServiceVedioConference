using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public class DeviceReceiveFilterItem<TRequestInfo>:IDeviceReceiveFilterItem<TRequestInfo>
        where TRequestInfo:IRequestInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceTypeItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IReceiveFilter<TRequestInfo> ReceiveFilter { get; set; }
    }
}