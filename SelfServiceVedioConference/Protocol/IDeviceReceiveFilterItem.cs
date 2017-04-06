using System;
using System.Security.Cryptography.X509Certificates;
using SelfServiceVedioConference.Device;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public interface IDeviceReceiveFilterItem        
    {

        /// <summary>
        /// Device Type
        /// </summary>
        string DeviceTypeItem { get; }

        /// <summary>
        /// Device Type Protocol 
        /// </summary>
       Type ReceiveFilter { get; }

        /// <summary>
        /// Device type Driver
        /// </summary>
        Type DeviceDriver { get; }
    }
}