using System;
using System.Linq;
using System.Net.Security;
using System.Windows.Forms.VisualStyles;
using SelfServiceVedioConference.config;
using SelfServiceVedioConference.Device;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference
{
    public class VedioConferenceAppServer:AppServer<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {

        public VedioConferenceConfig VedioConferenceConfig { get; set; }
        public IDeviceDriverFactory DeviceDriverFactory { get; set; }

        public VedioConferenceAppServer():base(new ProtocolReceiveFilterFactory<ReceiveFilterList,VedioConferenceRequestInfo>())
        {
            
        }

        protected override void OnStarted()
        {
            
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }


        protected override void OnNewSessionConnected(VedioConferenceAppSession session)
        {


            string remoteIp = session.RemoteEndPoint.Address.ToString();
            string remotePort = session.RemoteEndPoint.Port.ToString();

            foreach (var deviceConfigDeviceInRoom in this.VedioConferenceConfig.DeviceConfig.DeviceInRooms)
            {
                var device = deviceConfigDeviceInRoom.Devices.FirstOrDefault(t => t.DeviceIp.Contains(remoteIp) || t.DevicePort.Contains(remotePort));
                if (device != null && this.VedioConferenceConfig.DeviceReceiveFilterList.ResceiveFilterDic.ContainsKey(device.DeviceType))
                {
                    session.DeviceType = device.DeviceType;
                    session.DeviceRoom = deviceConfigDeviceInRoom.DeviceRoom;
                    session.DeviceIp = remoteIp;
                    session.Port = remotePort;
                    session.DeviceName = device.DeviceName;
                    session.DeviceDriver = DeviceDriverFactory.CreatDeviceDriver(this, session, device.DeviceType,device.DeviceDefaultConfig);
                    break;
                }
                else
                {
                    session.Close();
                    return;
                }
            }

            session.DeviceDriver.Init();

            base.OnNewSessionConnected(session);
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            VedioConferenceConfig=new VedioConferenceConfig();
            DeviceDriverFactory=new DeviceDriverFactory();
            var re=VedioConferenceConfig.Init(this);
            return base.Setup(rootConfig, config) &&re;
        }

    }
}