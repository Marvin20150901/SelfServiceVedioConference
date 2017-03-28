using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.Device;
using SelfServiceVedioConference.Protocol;
using Utilities.Configuration;

namespace SelfServiceVedioConference.config
{
    public class DeviceConfig:JSONConfig<DeviceConfig>
    {
        public string ConfigName { get; set; }
        
        public List<string> DeviceTypes { get; set; }

        public List<DeviceReceiveFilterTypeItem> DeviceReceiveFilterTypes { get; set; }

        public override string Name => "DeviceConfig";
        protected override string ConfigFileLocation => "./config/DeviceConfig.json";
    }


    public class DeviceReceiveFilterTypeItem
    {
        public string DeviceType { get; set; }

        public string ReceiveFilterType { get; set; }
    }


}
