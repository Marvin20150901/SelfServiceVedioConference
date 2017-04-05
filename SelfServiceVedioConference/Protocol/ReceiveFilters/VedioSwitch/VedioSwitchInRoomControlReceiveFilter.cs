using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol.ReceiveFilters.VedioSwitch
{
    public class VedioSwitchInRoomControlReceiveFilter: TerminatorReceiveFilter<VedioConferenceRequestInfo>
    {

        public static readonly string Terminator = @".";
        public static readonly string[] ParameterSpliters = { @" ", "\r\n" };
        public VedioSwitchInRoomControlReceiveFilter(byte[] terminator) : base(terminator)
        {

        }


        public VedioSwitchInRoomControlReceiveFilter() : this(Encoding.ASCII.GetBytes(Terminator))
        {
            
        }


        protected override VedioConferenceRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            //throw new System.NotImplementedException();
            VedioConferenceRequestInfo requestInfo = null;
            var session = this.Session as VedioConferenceAppSession;

            requestInfo =new VedioConferenceRequestInfo()
            {
                Key = session.DeviceType+@"_"+session.DeviceRoom+@"_VedioSwitchEvent",
                DeviceType = session.DeviceType,
                EventType = @"VedioSwitchEvent",
                Parameter = Encoding.ASCII.GetString(data,offset,length)
            };

            return requestInfo;

        }
    }
}