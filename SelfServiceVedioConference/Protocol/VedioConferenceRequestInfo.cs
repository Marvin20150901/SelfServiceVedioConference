using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    public class VedioConferenceRequestInfo:IRequestInfo
    {
        public string Key { get; set; }

        public string DeviceType { get; set; }

        public string EventType { get; set; }

        public string Parameter { get; set; }


    }
}