using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase;

namespace SelfServiceVedioConference
{
    public class VedioConferenceAppSession:AppSession<VedioConferenceAppSession,VedioConferenceRequestInfo>
    {
        public string DeviceType { get; set; }

        public string DeviceName { get; set; }

        public string DeviceRoom { get; set; }

//        public string DeviceId { get; set; }

        public string DeviceIp { get; set; }

        public string Port { get; set; }


        private bool m_AppendNewLineForResponse = false;

        private static string m_NewLine = "\r\n";


        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }


        protected override void HandleUnknownRequest(VedioConferenceRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {

        }

        /// <summary>
        /// Processes the sending message.
        /// </summary>
        /// <param name="rawMessage">The raw message.</param>
        /// <returns></returns>
        protected virtual string ProcessSendingMessage(string rawMessage)
        {
            if (!m_AppendNewLineForResponse)
                return rawMessage;

            if (AppServer.Config.Mode == SocketMode.Udp)
                return rawMessage;

            if (string.IsNullOrEmpty(rawMessage) || !rawMessage.EndsWith(m_NewLine))
                return rawMessage + m_NewLine;
            else
                return rawMessage;
        }



        public override void Send(string message)
        {
            base.Send(ProcessSendingMessage(message));
        }

        public override void Send(string message, params object[] paramValues)
        {
            base.Send(ProcessSendingMessage(message), paramValues);
        }

        
    }



}