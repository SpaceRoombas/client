using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillRobotPanel : MonoBehaviour
{
    public GameObject robotParent;
    public GameObject scrollViewContent;
    public GameObject panelPrefab;
    private float timepass;

    private int counter;
    void Start()
    {
        timepass = 0f;
        fillRobotPanel(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (timepass > 1) {
            updateRobotPanel();
            timepass = 0;
        }
        timepass += Time.deltaTime;
    }

    private void fillRobotPanel()
    {
        foreach (Transform child in robotParent.transform)
        {
            GameObject tile = Instantiate(panelPrefab) as GameObject;
            tile.transform.SetParent(scrollViewContent.transform, false);
            tile.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = child.name;
            
            RobotController robotController = child.GetComponent<RobotController>();
            tile.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = 
                "" + (int)robotController.transform.position.x + "," + (int)robotController.transform.position.y;

            string firmware = robotController.GetFirmware();
            tile.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = "Code: " + firmware;
            tile.GetComponent<RobotViewButton>().setNameFirmware(child.name, firmware);
        }
    }

    public void updateRobotPanel()
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

            RobotController robotController = child.GetComponent<RobotController>();
            tile.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text =
                "" + (int)robotController.transform.position.x + "," + (int)robotController.transform.position.y;

            string firmware = robotController.GetFirmware();
            tile.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = "Code: " + firmware;
            tile.GetComponent<RobotViewButton>().setNameFirmware(child.name, firmware);
        }
    }

    
}
