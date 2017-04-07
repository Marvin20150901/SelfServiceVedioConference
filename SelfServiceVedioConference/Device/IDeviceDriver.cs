using System.Collections.Generic;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public interface IDeviceDriver
    {
        string DeviceType { get; set; }

        bool Init();

        bool Heartbeat(int iTime);

        bool SetDefaultConfig();

        bool IsTimeoutConnect { get; set; }

        DeviceState DeviceState { get; set; }

        IAppSession Session { get; set; }
        IAppServer Server { get; set; }

        List<string> DefaultConfStrings { get; set; }
    }
}