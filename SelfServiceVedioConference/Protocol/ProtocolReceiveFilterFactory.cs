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

            var server = appServer as VedioConferenceAppServer;
            string remoteIp = remoteEndPoint.Address.ToString();
            string remotePort = remoteEndPoint.Port.ToString();

            foreach (var deviceConfigDeviceInRoom in server.VedioConferenceConfig.DeviceConfig.DeviceInRooms)
            {
                var device=deviceConfigDeviceInRoom.Devices.FirstOrDefault(t => t.DeviceIp.Contains(remoteIp) || t.DevicePort.Contains(remotePort));
                if (device!=null && server.VedioConferenceConfig.DeviceReceiveFilterList.ResceiveFilterDic.ContainsKey(device.DeviceType))
                {
                    return (IReceiveFilter<TRequestInfo>)Activator.CreateInstance(
                        server.VedioConferenceConfig.DeviceReceiveFilterList.ResceiveFilterDic[device.DeviceType].ReceiveFilter);
                }
            }

            //return (IReceiveFilter<TRequestInfo>) new CiscoInRoomControlReceiveFilter();
            return null;

        }
    }
}
