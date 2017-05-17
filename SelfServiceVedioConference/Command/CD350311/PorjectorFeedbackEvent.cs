using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class PorjectorFeedbackEvent : ICommand<VedioConferenceAppSession, VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new NotImplementedException();

            Console.WriteLine(requestInfo.Parameter);
        }

        public string Name => @"SharpD540XA_CD350311_PorjectorEvent";
    }
}
