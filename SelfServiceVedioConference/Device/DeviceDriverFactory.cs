using System;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference.Device
{
    public class DeviceDriverFactory:IDeviceDriverFactory
    {
        IDeviceDriver IDeviceDriverFactory.CreatDeviceDriver(IAppServer server, IAppSession session, string deviceType)
        {
            //throw new NotImplementedException();

            var appServer = server as VedioConferenceAppServer;
            if (appServer != null)
            {
                return
                    (IDeviceDriver)
                    Activator.CreateInstance(
                        appServer.VedioConferenceConfig.DeviceReceiveFilterList.ResceiveFilterDic[deviceType]
                            .DeviceDriver);
            }
            else
            {
                return null;
            }
        }

    }
}