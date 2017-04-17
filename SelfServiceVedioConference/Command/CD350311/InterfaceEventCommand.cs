using System.Linq;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase.Command;

namespace SelfServiceVedioConference.Command.CD350311
{
    public class InterfaceEventCommand:ICommand<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public void ExecuteCommand(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //throw new System.NotImplementedException();
            //session.Send("-----{0}---------------------{1}-----", session.DeviceType, session.DeviceRoom);、

            if (requestInfo.Parameter.Contains("Pressed Signal:\"H_InputSource"))
            {
                
                var s =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"VedioSwitch" && t.DeviceRoom == session.DeviceRoom);
                foreach (var conferenceAppSession in s)
                {
                    if (requestInfo.Parameter.Contains("Signal:\"H_InputSource:2\""))
                    {
                        conferenceAppSession.Send(@"1v2.");//input source is zhantai ,need change the vedioSwitch
                        session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_InputSource\" Value:\"2\"");
                    }
                    else
                    {
                        conferenceAppSession.Send(@"2v2.");//dafault is the computer
                        session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_InputSource\" Value:\"1\"");
                    }                                       
                }

                return;
            }

            if (requestInfo.Parameter.Contains("Clicked Signal:\"H_OnPorjector"))
            {
                session.Send(
                    "xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OnPorjector\" Value: \"active\"");
                session.Send(
                    "xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OffPorjector\" Value: \"inactive\"");

                var orDefault =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"SharpD540XA" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();
                orDefault?.Send(new byte[] {0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x31, 0x0D}, 0, 9);


                return;
            }

            if (requestInfo.Parameter.Contains("Clicked Signal:\"H_OffPorjector"))
            {
                session.Send(
                    "xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OffPorjector\" Value: \"active\"");
                session.Send(
                    "xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OnPorjector\" Value: \"inactive\"");

                var orDefault =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"SharpD540XA" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();
                orDefault?.Send(new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x30, 0x0D }, 0, 9);

                return;
            }

            if (requestInfo.Parameter.Contains("Pressed Signal:\"H_ConferenceMode"))
            {
                var inSource =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"VedioSwitch" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();
                var sharp =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"SharpD540XA" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();

                if (requestInfo.Parameter.Contains("Signal:\"H_ConferenceMode:1\""))
                {
                    session.Send(
                        "xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_ConferenceMode\" Value:\"1\"");
                    inSource?.Send(@"1V2.");
                    sharp?.Send(new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x31, 0x0D }, 0, 9);
                }
                else
                {
                    inSource?.Send(@"1V2.");
                    sharp?.Send(new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x30, 0x0D }, 0, 9);
                    session.Send(
                        "xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_ConferenceMode\" Value:\"2\"");
                }

                return;
            }

            session.AppServer.Logger.ErrorFormat("{0} get the unknow command: (1)", session.DeviceRoom,
                requestInfo.Parameter);

        }

        public string Name => @"CiscoMX800_CD350311_InterfaceEvent";
    }
}