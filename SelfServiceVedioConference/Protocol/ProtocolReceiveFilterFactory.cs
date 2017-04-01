using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.Protocol.ReceiveFilters.CiscoMxSxSeries;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SelfServiceVedioConference.Protocol
{
    class ProtocolReceiveFilterFactory<TReceiveFilterList, TRequestInfo>:IReceiveFilterFactory<TRequestInfo> 
        where TRequestInfo : IRequestInfo
        where TReceiveFilterList:IReceiveFilterList
    {

        //private IReceiveFilterFactory<TRequestInfo> _receiveFilterFactoryImplementation;
        public IReceiveFilter<TRequestInfo> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            //return _receiveFilterFactoryImplementation.CreateFilter(appServer, appSession, remoteEndPoint);
            
            return (IReceiveFilter<TRequestInfo>) new CiscoInRoomControlReceiveFilter();
        }
    }
}
