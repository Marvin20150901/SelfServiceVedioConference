using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class CommandReturnCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new System.NotImplementedException();
        }

        public string Name => @"CiscoMX800_CD350311_CommandReturn";
    }
}