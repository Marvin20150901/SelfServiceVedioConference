using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfServiceVedioConference.Device
{
    public class VedioSwitchDevice:IDeviceDriver
    {
        public string DeviceType { get; set; }

        public bool Init()
        {
            return false;
        }

        public bool Heartbeat(int iTime)
        {
            return false;
        }

        public bool SetDefaultConfig()
        {
            return false;
        }

        public bool IsTimeoutConnect { get; set; }

        public DeviceState DeviceState { get; set; }

        public IAppSession Session { get; set; }
        public IAppServer Server { get; set; }

        public List<string> DefaultConfStrings { get; set; }
    }
}
