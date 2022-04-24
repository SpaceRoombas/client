using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCode : MonoBehaviour
{
    public TMP_InputField code;
    public NetworkInterface network;
    public string robotName;

    private void Start()
    {
        robotName = "r0";
    }
    public void getCode()
    {
        GameObject r = GameObject.Find("Robots/"+robotName);
        r.GetComponent<RobotController>().SetFirmware(code.text);
        Debug.Log("submit code:"+robotName+code.text);
        network.SendFirmwareChange(robotName, code.text);
    }

    public void SetRobotName(string s) {
        robotName = s;
    }
}
