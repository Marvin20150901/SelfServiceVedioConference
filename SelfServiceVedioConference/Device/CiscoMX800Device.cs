using System.Collections.Generic;
using System.ComponentModel;
using SuperSocket.SocketBase;
using System.Threading.Tasks;
using System;

namespace SelfServiceVedioConference.Device
{
    public class CiscoMX800Device:IDeviceDriver
    {

        public int  HeartbeatTime { get; set; } = 150;
        public string DeviceType { get; set; }
        public bool Init()
        {
            //throw new System.NotImplementedException();
            var appSession = Session as VedioConferenceAppSession;
            var appServer = Server as VedioConferenceAppServer;

            IsTimeoutConnect = false;
            DeviceState = DeviceState.Open;
            DeviceType = appSession?.DeviceType;

            if (appSession!=null && appServer!=null)
            {
                appSession.Send("xCommand Peripherals Connect ID:\"{0}\" Type:ControlSystem",appServer.VedioConferenceConfig.DeviceConfig.ConfigName);
                appSession.Send("xCommand Peripherals HeartBeat ID:\"{0}\" Timeout: {1}", appServer.VedioConferenceConfig.DeviceConfig.ConfigName,HeartbeatTime+30);

                Heartbeat(HeartbeatTime);
                SetDefaultConfig();

                appSession.Send(@"xfeedback register event/UserInterface/Extensions/Event");
                return true;
            }

            return false;
        }


        public bool Heartbeat(int iTime)
        {
            //throw new System.NotImplementedException();
            Task.Run(() => {
                var appSession = Session as VedioConferenceAppSession;
                var appServer = Server as VedioConferenceAppServer;
                if (appSession!=null && appServer!=null)
                {
                    while (true)
                    {
                        appSession.Send("xCommand Peripherals HeartBeat ID:\"{0}\" Timeout: {1}", appServer.VedioConferenceConfig.DeviceConfig.ConfigName, HeartbeatTime + 30);
                        appSession.DeviceDriver.IsTimeoutConnect = true;
                        System.Threading.Thread.Sleep(iTime * 1000);
                        if(appSession.DeviceDriver.IsTimeoutConnect)
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


        }

        public CiscoMX800Device()
        {
            

        }

    }
}