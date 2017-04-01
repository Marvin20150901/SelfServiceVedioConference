using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.config
{
    public interface IVedioConferenceConfig
    {
        string LocalFile { get; }

        DeviceConfig DeviceConfig { get; }

        IReceiveFilterList DeviceReceiveFilterList { get; set; }

        bool Init(IAppServer appServer);
    }
}