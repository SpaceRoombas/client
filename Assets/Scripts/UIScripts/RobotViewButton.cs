using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotViewButton : MonoBehaviour
{
    public string robotName;
    public string firmware;
    public TMP_InputField input;
    
    public void MovetoCodeEditor()
    {
        Debug.Log("pushed to editor");
        GameObject text = GameObject.Find("Canvas/CodeEditor/Code Input");
        TMP_InputField field = text.GetComponent<TMP_InputField>();
        field.text = firmware;

        GameObject editor = GameObject.Find("Canvas/CodeEditor");
        editor.GetComponent<GetCode>().SetRobotName(robotName);

        // Camera code
        GameObject r = GameObject.Find("Robots/" + robotName);
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CameraMovement>().SetFocus(r.transform);
    }

    public void setNameFirmware(string n, string f)
    {
        robotName = n;
        firmware = f;
    }
}
