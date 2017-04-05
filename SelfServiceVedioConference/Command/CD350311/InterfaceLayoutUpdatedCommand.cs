using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class InterfaceLayoutUpdatedCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new System.NotImplementedException();
            session.Send("-----{0}---------------------{1}-----", session.DeviceType, session.DeviceRoom);
        }

        public string Name => "CiscoMX800_CD350311_InterfaceLayoutUpdated";
    }
}