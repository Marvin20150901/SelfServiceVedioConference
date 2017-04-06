using System.Collections.Generic;
using System.ComponentModel;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public class CiscoMX800Device:IDeviceDriver
    {
        public string DeviceType { get; set; }
        public bool Init()
        {
            //throw new System.NotImplementedException();


            return false;
        }

        public bool Heartbeat(int iTime)
        {
            throw new System.NotImplementedException();
        }

        public bool SetDefaultConfig()
        {
            //throw new System.NotImplementedException();

            if (DefaultConfStrings.Count>0)
            {
                foreach (var defaultConfString in DefaultConfStrings)
                {
                    (Session as VedioConferenceAppSession)?.Send(defaultConfString);
                }
            }

            return false;
        }

        public bool IsTimeoutConnect { get; set; }
        public DeviceState DeviceState { get; set; }
        public IAppSession Session { get; set; }
        public IAppServer Server { get; set; }
        public List<string> DefaultConfStrings { get; set; }



        public CiscoMX800Device(IAppSession session, IAppServer server, List<string> defaultConfigStringList)
        {
            Session = session;
            Server = server;
            DefaultConfStrings = defaultConfigStringList;

            IsTimeoutConnect = false;
            DeviceState = DeviceState.Open;
            DeviceType = (session as VedioConferenceAppSession)?.DeviceType;

        }


    }
}