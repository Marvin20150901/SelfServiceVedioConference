using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public class CiscoMX800Device:IDeviceDriver
    {
        public string DeviceType { get; }
        public bool Init()
        {
            throw new System.NotImplementedException();
        }

        public bool Heartbeat(bool bTodo, int iTime)
        {
            throw new System.NotImplementedException();
        }

        public bool SetDefaultConfig()
        {
            throw new System.NotImplementedException();
        }

        public bool IsTimeoutConnect { get; }
        public DeviceState DeviceState { get; }
        public IAppSession Session { get; }
        public IAppServer Server { get; }
    }
}