using ClientConnector.messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Network.Interfaces;
using StaticContext;

public class UpdateGameListButtons : MonoBehaviour, IGameListingHandler
{
    public GameObject listButtonPrefab;
    public GameObject scrollViewContent;
    private MatchConnectionDetails[] matchList;


    public void Awake()
    {
        matchList = new MatchConnectionDetails[4];
    }
    public void changeButtonInformation()
    {
        // remove existing buttons
        scrollViewContent.transform.DetachChildren();

        for (int i = 0; i < matchList.Length; i++)
        {
            GameObject button = Instantiate(listButtonPrefab) as GameObject;
            button.transform.SetParent(scrollViewContent.transform, false);
            button.GetComponentInChildren<TMP_Text>().text = (i + 1) + ".) Opponent(s): ";

            GameListButtonClick btnClickState = button.GetComponent<GameListButtonClick>();
            btnClickState.port = matchList[i].port;
            btnClickState.host = matchList[i].Host;

            for (int j = 0; j < matchList[i].Players.Length; j++) 
            {
                button.GetComponentInChildren<TMP_Text>().text += matchList[i].Players[j] + ", ";
            }
            
        }
    }

    public void TriggerFetchGameListing()
    {
        StartCoroutine(MatchmakingRequests.PerformGameListingRequest(AuthenticationContext.Username, AuthenticationContext.Token, this));
    }

    public void HandleGameList(GameListing listing)
    {
        matchList = listing.ConnectionDetails;

        changeButtonInformation();
    }

    public void HandleGameListingFailure(string reason)
    {
        Debug.LogError("Couldnt fetch game list");
    }
}
