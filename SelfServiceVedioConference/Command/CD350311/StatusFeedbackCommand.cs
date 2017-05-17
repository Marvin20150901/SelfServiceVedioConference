using System;
using System.Linq;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class StatusFeedbackCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new System.NotImplementedException();
            //session.Send(requestInfo.Parameter);

            Console.WriteLine(requestInfo.Parameter);

            if (requestInfo.Parameter.Contains(@"Standby") && requestInfo.Parameter.Contains(@"State: Off"))
            {
                //session.DeviceDriver.IsTimeoutConnect = false;

                //Console.WriteLine("Device standby off");

                foreach (var roomDevice in session.AppServer.GetAllSessions().Where(t=>t.DeviceRoom==session.DeviceRoom))
                {
                    roomDevice.DeviceDriver.SetDefaultConfig();
                }
                return;
            }

            if (requestInfo.Parameter.Contains(@"Standby") && requestInfo.Parameter.Contains(@"State: Standby"))
            {
                //session.DeviceDriver.IsTimeoutConnect = false;

                //Console.WriteLine("Device standby ");

                foreach (var roomDevice in session.AppServer.GetAllSessions().Where(t => t.DeviceRoom == session.DeviceRoom))
                {
                    roomDevice.DeviceDriver.SetCloseDefaultConfig();
                }
                return;
            }

        }

        public string Name => @"CiscoMX800_CD350311_StatusFeedback";
    }
}
