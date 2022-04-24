using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillRobotPanel : MonoBehaviour
{
    public GameObject robotParent;
    public GameObject scrollViewContent;
    public GameObject panelPrefab;


    private int counter;
    void Start()
    {
       fillRobotPanel(); 
    }

    // Update is called once per frame
    void Update()
    {
        updateRobotPanel();
    }

    private void fillRobotPanel()
    {
        foreach(Transform child in robotParent.transform)
        {
            GameObject tile = Instantiate(panelPrefab) as GameObject;
            tile.transform.SetParent(scrollViewContent.transform, false);
            tile.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = child.name;
            Debug.Log(tile.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text);
        }
    }

    private void updateRobotPanel()
    {
        foreach (Transform panelChild in scrollViewContent.transform)
        {
            Destroy(panelChild.gameObject);
        }
        foreach (Transform child in robotParent.transform)
        {
            GameObject tile = Instantiate(panelPrefab) as GameObject;
            tile.transform.SetParent(scrollViewContent.transform, false);
            tile.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = child.name;
        }
    }

    
}
