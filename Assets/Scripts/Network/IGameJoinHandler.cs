
namespace Network.Interfaces
{
    public interface IGameJoinHandler
    {
        void HandleGameJoin(string username, string token, string host, int port);
        void HandleJoinFailure(string reason);
    }
}
