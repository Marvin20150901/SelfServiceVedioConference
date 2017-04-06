using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public interface IDeviceDriver
    {
        string DeviceType { get; }

        bool Init();

        bool Heartbeat(bool bTodo, int iTime);

        bool SetDefaultConfig();

        bool IsTimeoutConnect { get; }

        DeviceState DeviceState { get; }

        IAppSession Session { get; }
        IAppServer Server { get; }

        string[] DefaultConfStrings { get; }
    }
}