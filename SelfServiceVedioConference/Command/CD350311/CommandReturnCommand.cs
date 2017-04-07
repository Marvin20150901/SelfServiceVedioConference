using System;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class CommandReturnCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new System.NotImplementedException();

            Console.WriteLine(requestInfo.Parameter);


            if (requestInfo.Parameter.Contains(@"PeripheralsHeartBeatResult (status=OK)"))
            {
                session.DeviceDriver.IsTimeoutConnect = false;
                return;
            }


        }

        public string Name => @"CiscoMX800_CD350311_CommandReturn";
    }
}