using UnityEngine;
using UnityEngine.SceneManagement;
using Network.Interfaces;
using StaticContext;

public class MenuHandler : MonoBehaviour, IGameJoinHandler
{
    public void HandleGameJoin(string username, string token, string host, int port)
    {
        Debug.Log($"Got Game join: {host}:{port}");
        GameConnectionContext.SetContext(username, token, host, port);

        SceneManager.LoadScene("Game Area");
    }

    public void HandleJoinFailure(string reason)
    {
        throw new System.NotImplementedException();
    }

    public void AttemptGameJoin()
    {
        Debug.Log("Sending game join request");
        StartCoroutine(MatchmakingRequests.PerformGameJoinRequest(AuthenticationContext.Username, AuthenticationContext.Token, this));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
