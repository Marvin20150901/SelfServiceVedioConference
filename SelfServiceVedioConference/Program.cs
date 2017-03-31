﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.config;
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

            /*
              Console.WriteLine("program has start...{0}", DateTime.Now);

              var appServer=new AppServer();

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


              appServer.NewSessionConnected += AppServer_NewSessionConnected;
              appServer.NewRequestReceived += AppServer_NewRequestReceived;


              while (Console.ReadKey().KeyChar!='Q')
              {
                  Console.WriteLine();
                  continue;
              }

              appServer.Stop();

              Console.WriteLine("the server has stoped");
              Console.ReadKey();
              */

             
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
            }
            else
            {
                Console.WriteLine("it is a error");
            }
            
            */


            string t = "\r\n** end";
            string o = "*e UserInterface Extensions Event Changed Signal: \"togglebutton:on\"\r\n** end";


            Console.WriteLine(o.IndexOf(t));




            Console.ReadLine();


        }

        private static void AppServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            switch (requestInfo.Key)
            {
                case ("ECHO"):
                    session.Send(requestInfo.Body);
                    break;

                case ("ADD"):
                    session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
                    break;

                case ("MULT"):

                    var result = 1;

                    foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
                    {
                        result *= factor;
                    }

                    session.Send(result.ToString());
                    break;

            }
        }

        private static void AppServer_NewSessionConnected(AppSession session)
        {
            session.Send("you have connected the server1");
        }
    }
}
