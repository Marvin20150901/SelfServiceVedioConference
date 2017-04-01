using System;
using System.Text;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol.ReceiveFilters.CiscoMxSxSeries
{
    public class CiscoInRoomControlReceiveFilter:TerminatorReceiveFilter<VedioConferenceRequestInfo>
    {


        public static readonly string Terminator = "\r\n** end";
        public static readonly string[] ParameterSpliters ={@" "};


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

            if (length!=0)
            {
                dataStr = Encoding.ASCII.GetString(data, offset, length);
                parasStr = dataStr.Split(ParameterSpliters, StringSplitOptions.RemoveEmptyEntries);

                if (parasStr[0]==@"*e")
                {
                    
                }
                else if (parasStr[0]==@"*s")
                {
                    
                }
                else
                {
                    
                }
            }


            return requestInfo;
        }
    }
}