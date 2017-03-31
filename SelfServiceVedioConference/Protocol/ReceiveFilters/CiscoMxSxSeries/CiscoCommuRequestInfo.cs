using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol.ReceiveFilters.CiscoMxSxSeries
{
    public class CiscoCommuRequestInfo : IRequestInfo
    {
        public string Key { get; set; }


        public string DeviceEvent { get; set; }


        public string DeviceWidget { get; set; }

        public string DeviceValue { get; set; }


        public string Parameter { get; set; }

        public CiscoCommuRequestInfo(string key, string deviceEvent, string deviceWidget, string deviceVlaue,
            string para)
        {

        }

        public CiscoCommuRequestInfo(string key, string deviceEvent, string para)
            : this(key, deviceEvent, string.Empty, string.Empty, para)
        {

        }

        public CiscoCommuRequestInfo()
        {
            
        }

    }
}