using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public interface IDeviceDriverFactory<out TDevice> where TDevice : IDeviceDriver
    {
        TDevice CreatDeviceDriver(IAppSession session, string deviceType);
    }


}