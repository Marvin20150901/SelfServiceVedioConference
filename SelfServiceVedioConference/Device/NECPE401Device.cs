using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    class NECPE401Device:IDeviceDriver
    {

        public string DeviceType { get; set; }
        public bool Init()
        {

            SetDefaultConfig();

            return true;
        }

        public bool Heartbeat(int iTime)
        {

            return true;
        }

        public bool SetDefaultConfig()
        {
            (Session as VedioConferenceAppSession)?.Send(new byte[] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x03}, 0, 6);

            if (DefaultConfStrings.Count > 0)
            {
                foreach (var defaultConfString in DefaultConfStrings)
                {
                    (Session as VedioConferenceAppSession)?.Send(defaultConfString);
                }

                return true;
            }

            return false;
        }


        //public


        public bool IsTimeoutConnect { get; set; } = false;
        public bool SetCloseDefaultConfig()
        {
            (Session as VedioConferenceAppSession)?.Send(new byte[] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x03 }, 0, 6);
            return true;
        }

        public DeviceState DeviceState { get; set; }
        public IAppSession Session { get; set; }
        public IAppServer Server { get; set; }
        public List<string> DefaultConfStrings { get; set; }
        public List<string> DefaultCloseConfStrings { get; set; }

        public NECPE401Device(IAppSession session, IAppServer server, List<string> defaultConfigStringList)
        {
            Session = session;
            Server = server;
            DefaultConfStrings = defaultConfigStringList;
        }

        public NECPE401Device()
        {

        }
    }
}
