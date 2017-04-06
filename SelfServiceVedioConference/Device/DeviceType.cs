namespace SelfServiceVedioConference.Device
{
    public enum DeviceType
    {
        CiscoMx800,
        CiscoMx300,
        SharpD540
    }



    public enum DeviceState
    {
        Open,
        Init,
        Work,
        Missing,
        Close    
    }
}