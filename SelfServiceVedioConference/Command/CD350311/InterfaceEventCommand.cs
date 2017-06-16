using System;
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

            Console.WriteLine(requestInfo.Parameter);
         

            if (requestInfo.Parameter.Contains("Pressed") && (requestInfo.Parameter.Contains("H_InputSource") || requestInfo.Parameter.Contains("G_InputSource")))
            {
                
                var s =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"VedioSwitch" && t.DeviceRoom == session.DeviceRoom);
                foreach (var conferenceAppSession in s)
                {
                    if (requestInfo.Parameter.Contains("H_InputSource:2") || requestInfo.Parameter.Contains("G_InputSource:2"))
                    {
                        conferenceAppSession.SendNoNewLine(@"7B1,7.");//input source is zhantai ,need change the vedioSwitch
                        session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_InputSource\" Value:\"2\"");
                        session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"G_InputSource\" Value:\"2\"");
                    }
                    else
                    {
                        conferenceAppSession.SendNoNewLine(@"1B1,7.");//dafault is the computer
                        session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_InputSource\" Value:\"1\"");
                        session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"G_InputSource\" Value:\"1\"");
                    }                                       
                }

                return;
            }

            //Porjector on contriol
            if (requestInfo.Parameter.Contains("Clicked") && (requestInfo.Parameter.Contains("H_OnPorjector")|| requestInfo.Parameter.Contains("G_OnPorjector")))
            {
                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OnPorjector\" Value: \"active\"");
                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OffPorjector\" Value: \"inactive\"");

                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OnPorjector\" Value: \"active\"");
                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OffPorjector\" Value: \"inactive\"");

                var orDefault =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"NECPE401" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();
                orDefault?.Send(new byte[] {0x02, 0x00, 0x00, 0x00, 0x00, 0x02}, 0, 6);


                return;
            }

            //Porjector off contriol
            if (requestInfo.Parameter.Contains("Clicked") && (requestInfo.Parameter.Contains("H_OffPorjector") || requestInfo.Parameter.Contains("G_OffPorjector")))
            {
                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OffPorjector\" Value: \"active\"");
                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OnPorjector\" Value: \"inactive\"");

                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OffPorjector\" Value: \"active\"");
                session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OnPorjector\" Value: \"inactive\"");

                var orDefault =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"NECPE401" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();
                orDefault?.Send(new byte[] {0x02, 0x01, 0x00, 0x00, 0x00, 0x03}, 0, 6);

                return;
            }

            //conference mode function
            if (requestInfo.Parameter.Contains("Released") && requestInfo.Parameter.Contains("H_ConferenceMode"))
            {
                var inSource =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"VedioSwitch" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();
                var sharp =
                    session.AppServer.GetSessions(
                        t => t.DeviceType == @"NECPE401" && t.DeviceRoom == session.DeviceRoom).FirstOrDefault();

                if (requestInfo.Parameter.Contains("H_ConferenceMode:1"))
                {
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_ConferenceMode\" Value:\"1\"");
                    inSource?.SendNoNewLine(@"1B1,7.");

                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_InputSource\" Value:\"1\"");
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"G_InputSource\" Value:\"1\"");

                    sharp?.Send(new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x02 }, 0, 6);

                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OnPorjector\" Value: \"active\"");
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OffPorjector\" Value: \"inactive\"");

                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OnPorjector\" Value: \"active\"");
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OffPorjector\" Value: \"inactive\"");
                }
                else
                {
                    inSource?.SendNoNewLine(@"1B1.");

                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_InputSource\" Value:\"1\"");
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"G_InputSource\" Value:\"1\"");

                    sharp?.Send(new byte[] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x03 }, 0, 6);
                    
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId:\"H_ConferenceMode\" Value:\"2\"");

                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OffPorjector\" Value: \"active\"");
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"H_OnPorjector\" Value: \"inactive\"");

                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OffPorjector\" Value: \"active\"");
                    session.Send("xCommand UserInterface Extensions Widget SetValue WidgetId: \"G_OnPorjector\" Value: \"inactive\"");
                }

                return;
            }

            session.AppServer.Logger.ErrorFormat("{0} get the unknow command: {1}", session.DeviceRoom,
                requestInfo.Parameter);

        }

        public string Name => @"CiscoMX800_CD350311_InterfaceEvent";
    }
}