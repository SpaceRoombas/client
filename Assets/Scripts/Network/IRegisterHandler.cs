
namespace Network.Interfaces
{
    public interface IRegisterHandler
    {
        void HandleRegister();
        void HandleRegisterFailure(string reason);
    }
}
