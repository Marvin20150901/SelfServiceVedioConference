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
            //return false;

            var appSession = Session as VedioConferenceAppSession;
            var appServer = Server as VedioConferenceAppServer;

            IsTimeoutConnect = false;
            DeviceState = DeviceState.Open;
            DeviceType = appSession?.DeviceType;

            if (appSession != null && appServer != null)
            {
                //appSession.SendNoNewLine(@"1B1.1B7.");
                //appSession.SendNoNewLine(@"1V1.");
                //appSession.Send("xCommand Peripherals HeartBeat ID:\"{0}\" Timeout: {1}", appServer.VedioConferenceConfig.DeviceConfig.ConfigName, HeartbeatTime + 30);

                Heartbeat(HeartbeatTime);
                SetDefaultConfig();

                //appSession.Send(@"xfeedback register event/UserInterface/Extensions/Event");
                return true;
            }

            return false;
        }

        public int HeartbeatTime { get; set; } = 150;

        public bool Heartbeat(int iTime)
        {
            Task.Run(() => {
                var appSession = Session as VedioConferenceAppSession;
                var appServer = Server as VedioConferenceAppServer;
                if (appSession != null && appServer != null)
                {
                    while (true)
                    {
                        appSession.SendNoNewLine(@"/*ready.");
                        appSession.DeviceDriver.IsTimeoutConnect = true;
                        System.Threading.Thread.Sleep(iTime * 1000);
                        if (appSession.DeviceDriver.IsTimeoutConnect)
                        {
                            appServer.Logger.ErrorFormat("device is connecd timeout -{0}--{1}", appSession.DeviceName, appSession.DeviceRoom);
                            appSession.Close(CloseReason.TimeOut);
                            break;
                        }
                    }
                }

            });
            return true;
        }

        public bool SetDefaultConfig()
        {
            var appSession = Session as VedioConferenceAppSession;
            appSession?.SendNoNewLine(@"1B1,7.");
            appSession?.SendNoNewLine(@"/:BellOff.");

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



        public bool IsTimeoutConnect { get; set; }

        public bool SetCloseDefaultConfig()
        {
            //throw new NotImplementedException();
            var appSession = Session as VedioConferenceAppSession;
            appSession?.SendNoNewLine(@"1B1,7.");
            return true;
        }

        public DeviceState DeviceState { get; set; }

        public IAppSession Session { get; set; }
        public IAppServer Server { get; set; }

        public List<string> DefaultConfStrings { get; set; }
        public List<string> DefaultCloseConfStrings { get; set; }


        public VedioSwitchDevice(IAppSession session, IAppServer server, List<string> defaultConfigStringList)
        {
            Session = session;
            Server = server;
            DefaultConfStrings=defaultConfigStringList;
        }

        public VedioSwitchDevice()
        {
            
        }
    }
}
