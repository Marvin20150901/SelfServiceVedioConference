using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.config;
using SelfServiceVedioConference.Protocol;
using SuperSocket;
using SuperSocket.SocketBase;
using SuperSocket.Common;
using SuperSocket.Facility;
using SuperSocket.SocketBase.Protocol;
using Utilities.IO;
using FileInfo = Utilities.IO.FileInfo;

namespace SelfServiceVedioConference
{
    class Program
    {
        static void Main(string[] args)
        {
           

            Console.WriteLine("program has start...{0}", DateTime.Now);

              var appServer=new VedioConferenceAppServer();

              if (! appServer.Setup(2010))
              {
                  Console.WriteLine("the server con't be setup.");
                  Console.ReadKey();
                  return;
              }

            
            if (!appServer.Start())
              {
                  Console.WriteLine("appServer con't be start.");
                  Console.ReadKey();
                  return;
              }

            foreach (var deviceInRoomItem in appServer.VedioConferenceConfig.DeviceConfig.DeviceInRooms)
            {
                Console.WriteLine("--------{0}--------", deviceInRoomItem.DeviceRoom);
                foreach (var devicesItem in deviceInRoomItem.Devices)
                {
                    Console.WriteLine("{0}-----------{1}", devicesItem.DeviceName, devicesItem.DeviceType);
                }
            }

            foreach (var receiveFilter in appServer.VedioConferenceConfig.DeviceReceiveFilterList.ResceiveFilterDic)
            {
                    Console.WriteLine("*******{0}-----{1}*****",receiveFilter.Key,receiveFilter.Value);
            }

            Console.WriteLine("it's Ok");


//              appServer.NewSessionConnected += AppServer_NewSessionConnected;
//              appServer.NewRequestReceived += AppServer_NewRequestReceived;


              while (Console.ReadKey().KeyChar!='Q')
              {
                  Console.WriteLine();
                  continue;
              }

              appServer.Stop();

              Console.WriteLine("the server has stoped");
              Console.ReadKey();
   

             
//            Console.WriteLine("{0}",temp.DeviceReceiveFilterTypes);


/*         
          var jsonFile=new FileInfo(@"./config/DeviceConfig.json");
            if (jsonFile.Exists)
            {
                Console.WriteLine("have it");
            }
            else
            {
                Console.WriteLine("dont have it");
            }

            

            var jsonSerialize=new Utilities.IO.Serializers.Default.JSONSerializer();
            var deviceConfig = jsonSerialize.Deserialize(typeof(DeviceConfig), jsonFile);

            DeviceConfig dv=deviceConfig as DeviceConfig;
            if (dv !=null)
            {
                Console.WriteLine("{0}", dv.DeviceReceiveFilterTypes[0].ReceiveFilterType);

                foreach (var deviceInRoomItem in dv.DeviceInRooms)
                {
                    Console.WriteLine("--------{0}--------",deviceInRoomItem.DeviceRoom);
                    foreach (var devicesItem in deviceInRoomItem.Devices)
                    {
                        Console.WriteLine("{0}-----------{1}",devicesItem.DeviceName,devicesItem.DeviceType);
                    }
                }
            }
            else
            {
                Console.WriteLine("it is a error");
            }
            
            
*/

//            string t = "\r\n** end";
//            string o = "*e UserInterface Extensions Event Changed Signal: \"togglebutton:on\"\r\n** end";


//            Console.WriteLine(o.IndexOf(t));
            






            Console.ReadLine();


        }

        private static void AppServer_NewRequestReceived(VedioConferenceAppSession session, VedioConferenceRequestInfo requestInfo)
        {
            //session.Send("{0},    {1}", session.DeviceType, session.DeviceRoom);
            session.Send("{0} ----{1}-------{2}",requestInfo.Key,requestInfo.EventType,requestInfo.Parameter);
        }

        private static void AppServer_NewSessionConnected(VedioConferenceAppSession session)
        {
            session.Send("{0},    {1}\r\n",session.DeviceType,session.DeviceRoom);
        }
    }
}

