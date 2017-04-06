using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public interface IDeviceDriverFactory
    {
        IDeviceDriver CreatDeviceDriver(IAppServer server, IAppSession session, string deviceType);
    }


}