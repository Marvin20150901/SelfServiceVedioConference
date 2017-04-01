using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SelfServiceVedioConference.Device;
using SelfServiceVedioConference.Protocol;
using Utilities.Configuration;

namespace SelfServiceVedioConference.config
{




    [DataContract]
    public class DeviceReceiveFilterTypesItem
    {

        /// <summary>
        /// 
        /// </summary>
         
        [DataMember]
        public string DeviceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        
        [DataMember]
        public string ReceiveFilterType { get; set; }
    }

    [DataContract]
    public class DevicesItem
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DeviceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DeviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DeviceIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DevicePort { get; set; }
    }

    [DataContract]
    public class DeviceInRoomItem
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DeviceRoom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<DevicesItem> Devices { get; set; }
    }



    [DataContract]
    public class DeviceConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ConfigName { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        [DataMember]
        public List<string> DeviceTypes { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<string> ReceiveFilterTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<DeviceReceiveFilterTypesItem> DeviceReceiveFilterTypes { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<DeviceInRoomItem> DeviceInRooms { get; set; }


    }


}
