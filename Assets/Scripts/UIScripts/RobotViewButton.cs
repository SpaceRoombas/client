using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotViewButton : MonoBehaviour
{
    public string name;
    public string firmware;
    public TMP_InputField input;
    
    public void MovetoCodeEditor()
    {
        Debug.Log("pushed to editor");
        GameObject text = GameObject.Find("Canvas/CodeEditor/Code Input");
        TMP_InputField field = text.GetComponent<TMP_InputField>();
        field.text = firmware;
    }

    public void setNameFirmware(string n, string f)
    {
        
        name = n;
        firmware = f;
    }
}
