using System.Net.Security;
using System.Windows.Forms.VisualStyles;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference
{
    public class VedioConferenceAppServer:AppServer<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {


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


       
    }
}