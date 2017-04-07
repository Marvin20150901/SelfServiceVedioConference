using System;
using System.Collections.Generic;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public class DeviceDriverFactory:IDeviceDriverFactory
    {
        public IDeviceDriver CreatDeviceDriver(IAppServer server, IAppSession session, string deviceType, List<string> defaultSetStrings)
        {
            //throw new NotImplementedException();

            var appServer = server as VedioConferenceAppServer;
            if (appServer != null)
            {
                var deviceDriver =(IDeviceDriver) Activator.CreateInstance(
                    appServer.VedioConferenceConfig.DeviceReceiveFilterList.ResceiveFilterDic[deviceType]
                        .DeviceDriver);
                deviceDriver.Server = server;
                deviceDriver.Session = session;
                deviceDriver.DefaultConfStrings = defaultSetStrings;
                return deviceDriver;
            }
            else
            {
                return null;
            }
        }

    }
}