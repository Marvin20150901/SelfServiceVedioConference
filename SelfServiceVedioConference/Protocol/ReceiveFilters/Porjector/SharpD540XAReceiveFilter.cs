using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol.ReceiveFilters.Porjector
{
    class SharpD540XAReceiveFilter : TerminatorReceiveFilter<VedioConferenceRequestInfo>
    {

        public static readonly string Terminator = @".";
        public static readonly string[] ParameterSpliters = { @" ", "\r\n" };
        public SharpD540XAReceiveFilter(byte[] terminator) : base(terminator)
        {
        }

        public SharpD540XAReceiveFilter() : this(Encoding.ASCII.GetBytes(Terminator))
        {

        }

        protected override VedioConferenceRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            //throw new NotImplementedException();

            VedioConferenceRequestInfo requestInfo = null;
            var session = this.Session as VedioConferenceAppSession;

            requestInfo = new VedioConferenceRequestInfo()
            {
                Key = session.DeviceType + @"_" + session.DeviceRoom + @"_PorjectorEvent",
                DeviceType = session.DeviceType,
                EventType = @"PorjectorEvent",
                Parameter = Encoding.ASCII.GetString(data, offset, length)
            };

            return requestInfo;
        }
    }
}
