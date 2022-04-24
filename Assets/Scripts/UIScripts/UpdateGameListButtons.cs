using ClientConnector.messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateGameListButtons : MonoBehaviour
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
       

        string[] opponent1 = { "cody", "andersen" };
        string[] opponent2 = { "klayton" };
        string[] opponent3 = { "enrique" };
        string[] opponent4 = { "time" };

        matchList[0] = new MatchConnectionDetails("tessa", 2, opponent1);
        matchList[1] = new MatchConnectionDetails("andersen", 3, opponent2);
        matchList[2] = new MatchConnectionDetails("cody", 1, opponent3);
        matchList[3] = new MatchConnectionDetails("cody", 1, opponent4);


        for (int i = 0; i < matchList.Length; i++)
        {
            GameObject button = Instantiate(listButtonPrefab) as GameObject;
            button.transform.SetParent(scrollViewContent.transform, false);
            button.GetComponentInChildren<TMP_Text>().text = (i + 1) + ".) Opponent(s): ";

            GameListButtonClick btnPort = button.GetComponent<GameListButtonClick>();
            btnPort.port = matchList[i].port;

            for (int j = 0; j < matchList[i].Players.Length; j++) 
            {
                button.GetComponentInChildren<TMP_Text>().text += matchList[i].Players[j] + " ";
            }
            
            
        }
    }
}
