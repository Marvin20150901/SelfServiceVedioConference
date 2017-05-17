using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public class SharpD540XADevice:IDeviceDriver
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
            (Session as VedioConferenceAppSession)?.Send(new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x30, 0x0D }, 0, 9);

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

        public bool IsTimeoutConnect { get; set; } = false;
        public bool SetCloseDefaultConfig()
        {
            (Session as VedioConferenceAppSession)?.Send(new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x30, 0x0D }, 0, 9);
            return true;
        }

        public DeviceState DeviceState { get; set; }
        public IAppSession Session { get; set; }
        public IAppServer Server { get; set; }
        public List<string> DefaultConfStrings { get; set; }
        public List<string> DefaultCloseConfStrings { get; set; }


        public SharpD540XADevice(IAppSession session, IAppServer server, List<string> defaultConfigStringList)
        {
            Session = session;
            Server = server;
            DefaultConfStrings = defaultConfigStringList;
        }

        public SharpD540XADevice()
        {
            
        }
    }
}
