
namespace ClientConnector.messages
{
    public interface ICarrierPigeon
    {
        string GetPayloadType();
        string GetMessageType();

        void SetPayloadObject(object payload);
    }
}
