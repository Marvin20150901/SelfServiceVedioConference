namespace SelfServiceVedioConference.Device
{
    public interface IDeviceDriver
    {
        string DeviceType { get; }

        bool Init();

        bool Heartbeat(bool bTodo, int iTime);

        bool OpenDevice();

        bool CloseDevice();
    }
}