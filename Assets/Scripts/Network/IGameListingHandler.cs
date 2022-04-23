using ClientConnector.messages;

namespace Network.Interfaces
{
    public interface IGameListingHandler
    {
        void HandleGameList(GameListing listing);
        void HandleGameListingFailure(string reason);
    }
}
