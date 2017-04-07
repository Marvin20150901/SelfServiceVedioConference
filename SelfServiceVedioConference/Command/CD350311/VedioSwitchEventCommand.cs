using System;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class VedioSwitchEventCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
           //throw new System.NotImplementedException();
           Console.WriteLine(requestInfo.Parameter+@"-------vedio conference");
        }


        public string Name => "VedioSwitch_CD350311_VedioSwitchEvent";
    }
}