using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SelfServiceVedioConference.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using Utilities.IO;

namespace SelfServiceVedioConference.config
{
    public class VedioConferenceConfig:IVedioConferenceConfig
    {
        public string LocalFile { get; set; }
        public DeviceConfig DeviceConfig { get; set; }

        public IAppServer AppServer { get; set; }


        public IReceiveFilterList DeviceReceiveFilterList { get; set; }
        public VedioConferenceConfig():this(@"./config/DeviceConfig.json")
        {

        }


        public VedioConferenceConfig(string localFile)
        {
            LocalFile = localFile;
            DeviceReceiveFilterList=new ReceiveFilterList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appServer"></param>
        /// <returns></returns>
        public bool Init(IAppServer appServer)
        {
            DeviceConfig = null;
            AppServer =appServer as VedioConferenceAppServer;
            if (AppServer==null)
            {
                return false;
            }
            var jsonFile = new FileInfo(@"./config/DeviceConfig.json");
            if (!jsonFile.Exists)
            {
                AppServer.Logger.Error("The Config file don't exist!");
                return false;
            }
            
            var jsonSerialize = new Utilities.IO.Serializers.Default.JSONSerializer();
            var deviceConfig = jsonSerialize.Deserialize(typeof(DeviceConfig), jsonFile);

            DeviceConfig = deviceConfig as DeviceConfig;
            if (DeviceConfig == null)
            {
                AppServer.Logger.Error("Deserialize the config file has error!");
                return false;
            }

            return LoadReceiveFilter();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LoadReceiveFilter()
        {
            var commandAssemblies = new List<Assembly>();

            if (AppServer.GetType().Assembly != this.GetType().Assembly)
                commandAssemblies.Add(AppServer.GetType().Assembly);

            if (!commandAssemblies.Any())
            {
                commandAssemblies.Add(Assembly.GetEntryAssembly());
            }

            var typeInAppServer=new List<Type>();

            foreach (var commandAssembly in commandAssemblies)
            {
                typeInAppServer.AddRange(commandAssembly.GetTypes());
            }

            foreach (var deviceConfigDeviceReceiveFilterType in DeviceConfig.DeviceReceiveFilterTypes)
            {
                var receiveFilter =
                    typeInAppServer
                        .FirstOrDefault(t => t.FullName.Contains(deviceConfigDeviceReceiveFilterType.ReceiveFilterType));
                if (receiveFilter!=null)
                {
                    DeviceReceiveFilterList.AddDeviceRecieveFilterr(new DeviceReceiveFilterItem(deviceConfigDeviceReceiveFilterType.DeviceType,receiveFilter));
                }
                else
                {
                    AppServer.Logger.Error("DLL don't have the one device receivefilter");
                    return false;
                }
            }

            return true;

        }
    }
}