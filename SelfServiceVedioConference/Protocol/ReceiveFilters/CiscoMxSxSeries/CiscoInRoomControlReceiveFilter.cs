using System;
using System.Text;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol.ReceiveFilters.CiscoMxSxSeries
{
    public class CiscoInRoomControlReceiveFilter:TerminatorReceiveFilter<VedioConferenceRequestInfo>
    {


        public static readonly string Terminator = "\r\n** end";
        public static readonly string[] ParameterSpliters ={@" ","\r\n"};
        public static readonly string BegingTerminator = @"*";

        public static readonly string EventsOfWidgetAction = "*e UserInterface Extensions Event";
        public static readonly string EventsForPanelUpdate = "*e UserInterface Extensions Widget LayoutUpdated";


        public CiscoInRoomControlReceiveFilter(byte[] terminator) : base(terminator)
        {

        }

        public CiscoInRoomControlReceiveFilter(string terminator) : base(Encoding.ASCII.GetBytes(terminator))
        {
            
        }

        public CiscoInRoomControlReceiveFilter() : this(Terminator)
        {
            
        }

        protected override VedioConferenceRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            //throw new System.NotImplementedException();
            string dataStr = string.Empty;
            string[] parasStr=new string[] {};
            VedioConferenceRequestInfo requestInfo = null;
            var session = this.Session as VedioConferenceAppSession;

            if (length!=0)
            {
                dataStr = Encoding.ASCII.GetString(data, offset, length);

                if (dataStr.IndexOf(BegingTerminator,StringComparison.Ordinal)<0)
                {
                    requestInfo = new VedioConferenceRequestInfo();
                    return requestInfo;
                }
                dataStr = dataStr.Substring(dataStr.IndexOf(BegingTerminator, StringComparison.Ordinal));
                parasStr = dataStr.Split(ParameterSpliters, StringSplitOptions.RemoveEmptyEntries);

                if (session==null)
                {
                    requestInfo = new VedioConferenceRequestInfo();
                }
                else
                {
                    if (parasStr[0] == @"*e")
                    {
                        if (dataStr.Contains(EventsOfWidgetAction))
                        {

                            //Command key:Devicetype_DeviceRoom_EventType
                            requestInfo = new VedioConferenceRequestInfo()
                            {
                                Key = session.DeviceType + @"_" + session.DeviceRoom + @"_" + @"InterfaceEvent",
                                DeviceType = session.DeviceType,
                                EventType = @"InterfaceEvent",
                                Parameter = dataStr
                            };
                        }
                        else if (dataStr.Contains(EventsForPanelUpdate))
                        {
                            requestInfo = new VedioConferenceRequestInfo()
                            {

                                Key = session.DeviceType + @"_" + session.DeviceRoom + @"_InterfaceLayoutUpdated",
                                DeviceType = session.DeviceType,
                                EventType = @"InterfaceLayoutUpadted",
                                Parameter = dataStr
                            };
                        }
                        else
                        {
                            requestInfo = new VedioConferenceRequestInfo()
                            {
                                Key = session.DeviceType + @"_" + session.DeviceRoom + @"_EventFeedback",
                                DeviceType = session.DeviceType,
                                EventType = @"EventFeedback",
                                Parameter = dataStr
                            };
                        }

                    }
                    else if (parasStr[0] == @"*s")
                    {
                        requestInfo = new VedioConferenceRequestInfo()
                        {
                            Key = session.DeviceType + @"_" + session.DeviceRoom + @"_" + parasStr[1],
                            DeviceType = session.DeviceType,
                            EventType = parasStr[1],
                            Parameter = dataStr
                        };
                    }
                    else if (parasStr[0]==@"*r")
                    {
                        requestInfo=new VedioConferenceRequestInfo()
                        {
                            Key = session.DeviceType+@"_"+session.DeviceRoom+@"_CommandReturn",
                            DeviceType = session.DeviceType,
                            EventType = @"CommandReturn",
                            Parameter = dataStr
                        };
                    }
                    else
                    {
                        requestInfo = new VedioConferenceRequestInfo()
                        {
                            Key = session.DeviceType+@"_"+@"UnknowCommand"
                        };
                    }
                }
            }

                
            return requestInfo;
        }
    }
}