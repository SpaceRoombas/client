using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HandleLogin : MonoBehaviour, ILoginResponseHandler
{
    public TMP_InputField UsernameField;
    public TMP_InputField PasswordField;
    public TMP_Text StatusText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptLogin()
    {

        string user = UsernameField.text;
        string pass = PasswordField.text;

        Debug.Log($"Attempt login with: {user}:{pass}");
        setStatusMessage("Logging in..");
        StartCoroutine(MatchmakingRequests.PerformLoginRequest(user, pass, this));

        // Set some sort of "Please Wait" message
    }

    public void LoginSuccess(string username, string token)
    {
        Debug.Log("Authorized User");
        // set global state
        AuthenticationContext.Username = username;
        AuthenticationContext.Token = token;

        // switch scenes
        SceneManager.LoadScene("Menu");
    }

    public void LoginFailure(string reason)
    {
        Debug.LogError($"Failed to login player: {reason}");
        setStatusError(reason);
    }


    private void setStatusMessage(string message)
    {
        StatusText.color = Color.white;
        StatusText.text = message;
    }

    private void setStatusError(string errorMsg)
    {
        StatusText.color = Color.red;
        StatusText.text = errorMsg;
    }


}
