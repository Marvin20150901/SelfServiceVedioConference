using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class StatusFeedbackCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new System.NotImplementedException();
            session.Send(requestInfo.Parameter);
        }

        public string Name => @"CiscoMX800_CD350311_StatusFeedback";
    }
}