using UnityEngine.SceneManagement;
using UnityEngine;
using StaticContext;

public class GameListButtonClick : MonoBehaviour
{

    [HideInInspector]
    public int port;
    [HideInInspector]
    public string host;

    public void OpenGame()
    {
        GameConnectionContext.SetContext(AuthenticationContext.Username, AuthenticationContext.Token, host, port);

        SceneManager.LoadScene("Game Area");
    }
    

}
