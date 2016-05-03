namespace PetrofexSystem.Messaging
{
    public enum MessageType
    {
        Hello = 1,
        PubKey = 2,
        Start = 3,
        StartOk = 4,
        Connected = 5,
        Disconnect = 6,
        Error = 7,
        Ack = 16
    }

}