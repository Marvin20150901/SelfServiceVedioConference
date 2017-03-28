using System.Security.Cryptography.X509Certificates;
using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public interface IDeviceReceiveFilterItem<TRequestInfo>
        where TRequestInfo:IRequestInfo
    {

        /// <summary>
        /// Device Type
        /// </summary>
        string DeviceTypeItem { get; }

        /// <summary>
        /// Device Type Protocol 
        /// </summary>
        IReceiveFilter<TRequestInfo> ReceiveFilter { get; }
    }
}