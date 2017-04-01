using System;
using System.Net.Security;
using System.Windows.Forms.VisualStyles;
using SelfServiceVedioConference.config;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace SelfServiceVedioConference
{
    public class VedioConferenceAppServer:AppServer<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {

        public VedioConferenceConfig VedioConferenceConfig { get; set; }

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
            base.OnNewSessionConnected(session);
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            VedioConferenceConfig=new VedioConferenceConfig();
            VedioConferenceConfig.Init(this);
            return base.Setup(rootConfig, config);
        }

    }
}